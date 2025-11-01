mergeInto(LibraryManager.library, {
	
	InitYandexSDKExtern: function() {
		console.log("InitYandexSDKExtern() started");

		var self = this;

        YaGames.init().then(_ysdk => {
            window.ysdk = _ysdk;
            console.log("Yandex SDK initialized");

            // if (ysdk.features.LoadingAPI == undefined) {
            // 	return;
            // }

        	// ysdk.features.LoadingAPI.ready();
        
        	// Автоматически проверяем статус авторизации после инициализации
        	self.CheckAuthStatus();
        });
    },
    
    // Проверка статуса авторизации
    CheckAuthStatus: function() {
    	console.log("CheckAuthStatus() started");

        if (!ysdk) {
			return { authorized: false };
		}
        
        const status = {
            authorized: ysdk.auth.isAuthorized(),
            player: null
        };
        
        if (status.authorized) {
            status.player = {
                id: ysdk.player.getUniqueID(),
                name: ysdk.player.getName()
            };
        }
        
        // Отправляем статус в Unity
        if (myGameInstance !== undefined) {
            const jsonStatus = JSON.stringify(status);
            myGameInstance.SendMessage('Yandex', 'OnAuthStatusUpdated', jsonStatus);
        }
        
        return status;
    },
	
	RequestAuthorizationExtern: function() {
		console.log("RequestAuthorizationExtern() started");

		if (!ysdk) {
			console.error("RequestAuthorizationExtern: ysdk not initialized");
		}

		var self = this;

		return ysdk.auth.openAuthDialog()
            .then(() => self.CheckAuthStatus())
            .then(() => {
                // Статус будет обновлен через checkAuthStatus
                console.log("RequestAuthorizationExtern: OK");
            })
            .catch(error => {
                //const ptr = allocate(intArrayFromString(error.toString()), 'i8', ALLOC_NORMAL);
                //myGameInstance.SendMessage('Yandex', 'OnAuthFailedCallback', ptr);
                //_free(ptr);
            });;

        // this.RequestAuthorization()
        //     .then(() => {
        //         // Статус будет обновлен через checkAuthStatus
        //     })
        //     .catch(error => {
        //         //const ptr = allocate(intArrayFromString(error.toString()), 'i8', ALLOC_NORMAL);
        //         //myGameInstance.SendMessage('Yandex', 'OnAuthFailedCallback', ptr);
        //         //_free(ptr);
        //     });
	},
	
	// Инициализация API загрузки
	LoadingApiReadyExtern: function() {
		console.log("LoadingApiReadyExtern() started");
		
		if (ysdk === undefined)
		{
			console.log("LoadingApiReadyExtern: ysdk is undefined!");
			return;
		}
		
		if (ysdk.features.LoadingAPI == undefined) {
			console.log("LoadingApiReadyExtern: LoadingAPI is undefined!");
            return;
        }

		ysdk.features.LoadingAPI.ready();
	},
	
	// Управление игровым процессом
	GameplayApiStartExtern: function() {
		console.log("GameplayApiStartExtern() started");
		
		if (ysdk === undefined)
		{
			console.log("GameplayApiStartExtern: ysdk is undefined!");
			return;
		}

		if (ysdk.features.GameplayAPI == undefined) {
			console.log("GameplayApiStartExtern: GameplayAPI is undefined!");
            return;
        }
		
		ysdk.features.GameplayAPI.start();
	},
	
	GameplayApiStopExtern: function() {
		console.log("GameplayApiStopExtern() started");
		
		if (ysdk === undefined)
		{
			console.log("GameplayApiStopExtern: ysdk is undefined!");
			return;
		}
		
		if (ysdk.features.GameplayAPI == undefined) {
			console.log("GameplayApiStopExtern: GameplayAPI is undefined!");
            return;
        }

		ysdk.features.GameplayAPI.stop();
	},

	//FetchPlayerDataExtern: function() {
	//	console.log("FetchPlayerDataExtern() started");
	//	
	//	if (player === undefined)
	//	{
	//		console.log("FetchPlayerDataExtern: player is undefined!");
	//		return;
	//	}
	//	
	//	var playerName = player.getName();
	//	var photoMedium = player.getPhoto("medium");
	//	
	//	console.log("playerName: " + playerName);
	//	console.log("photoMedium: " + photoMedium);
	//	
	//	if (myGameInstance === undefined)
	//	{
	//		console.log("FetchPlayerDataExtern: myGameInstance is undefined!");
	//		return;
	//	}
	//
	//	myGameInstance.SendMessage("Yandex", "SetPlayerName", playerName);
	//	myGameInstance.SendMessage("Yandex", "SetAvatar", photoMedium);
	//},
	
	RateGameExtern: function() {
		console.log("RateGameExtern() started")
		
		if (ysdk === undefined)
		{
			console.log("RateGameExtern: ysdk is undefined!");
			return;
		}
		
		ysdk.feedback.canReview().then(({ value, reason }) => {
			if (value) {
				ysdk.feedback.requestReview()
					.then(({ feedbackSent }) => {
						console.log(feedbackSent);
					})
			}
			else {
				console.log(reason)
			}
		})
	},
	
	SaveExtern: function(data) {
		console.log("SaveExtern(data) started");
		
		var dataString = UTF8ToString(data);
		var myObj = JSON.parse(dataString);
		console.log(myObj);

		if (player === undefined)
		{
			console.log("SaveExtern: player is undefined!");
			return;
		}

		player.setData(myObj);
	},
	
	LoadExtern: function() {
		console.log("LoadExtern() started");
		
		if (player === undefined)
		{
			console.log("LoadExtern: player is undefined!");
			return;
		}
	
		player.getData().then(_data => {
			const myJSON = JSON.stringify(_data);
			myGameInstance.SendMessage("Progress", "SetPlayerInfo", myJSON);
		});
	},
	
	SetToLeaderboard: function(value) {
		console.info("SetToLeaderboard(value) started");
		
		if (!ysdk || !ysdk.auth.isAuthorized()) {
            console.warn("Cannot set leaderboard score - not authorized");
            return Promise.reject("Not authorized");
        }
		
		console.info("SetToLeaderboard: value: " + value);
		
		ysdk.leaderboards.setScore('Score', value);
		
		//ysdk.getLeaderboards()
		//	.then(lb => {
		//		lb.setLeaderboardScore('Score', value);
		//	});
	},
	
	GetLang: function() {
		console.log("GetLang() started");
		
		if (ysdk === undefined)
		{
			console.log("GetLang: ysdk is undefined!");
			return;
		}
		
		var lang = ysdk.environment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang, buffer, bufferSize);
		return buffer;
	},
	
	ResurrectExtern: function() {
		console.log("ResurrectExtern() started");
		
		if (ysdk === undefined)
		{
			console.log("ResurrectExtern: ysdk is undefined!");
			return;
		}
		
		var isRewarded = false;
		
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open.');
				},
				onRewarded: () => {
					console.log('Rewarded!');
					isRewarded = true;
				},
				onClose: () => {
					console.log('Video ad closed.');
					if (isRewarded) {
						myGameInstance.SendMessage("GameManager", "Resurrect");
					}
					else {
						myGameInstance.SendMessage("GameManager", "LoadFirstLevel");
					}
				},
				onError: (e) => {
					console.log('Error while open video ad:', e);
				}
			}
		})
	}
	
});