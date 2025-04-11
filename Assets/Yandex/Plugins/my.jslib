mergeInto(LibraryManager.library, {

	FetchPlayerDataExtern: function() {
		console.log("FetchPlayerDataExtern() started");
		
		if (player === undefined)
		{
			console.log("FetchPlayerDataExtern: player is undefined!");
			return;
		}
		
		console.log(player.getName());
		console.log(player.getPhoto("medium"));
		
		if (myGameInstance === undefined)
		{
			console.log("FetchPlayerDataExtern: myGameInstance is undefined!");
			return;
		}

		myGameInstance.SendMessage("Yandex", "SetPlayerName", player.getName());
		myGameInstance.SendMessage("Yandex", "SetAvatar", player.getPhoto("medium"));
  },
	
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
		} else {
				console.log(reason)
			}
		})
  },
	
	SaveExtern: function(data) {
		console.log("SaveExtern(data) started");
		
    var dataString = UTF8ToString(data);
		var myObj = JSON.parse(dataString);
		console.log(myObj);
		player.setData(myObj);
  },
	
	LoadExtern: function() {
		console.log("LoadExtern() started");
		
		if (player == undefined)
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
		
		if (ysdk === undefined)
		{
			console.log("SetToLeaderboard: ysdk is undefined!");
			return;
		}
		
		console.info("SetToLeaderboard: value: " + value);
		
		ysdk.getLeaderboards()
			.then(lb => {
				lb.setLeaderboardScore('Score', value);
			});
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
		
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open.');
				},
				onRewarded: () => {
					console.log('Rewarded!');
					myGameInstance.SendMessage("GameManager", "Resurrect");
				},
				onClose: () => {
					console.log('Video ad closed.');
					myGameInstance.SendMessage("GameManager", "LoadFirstLevel");
				},
				onError: (e) => {
					console.log('Error while open video ad:', e);
				}
			}
		})
	}
	
});