mergeInto(LibraryManager.library, {

	FetchPlayerDataExtern: function() {
		if (player == undefined)
		{
			console.log("FetchPlayerDataExtern: player is undefined");
			return;
		}
		
		console.log(player.getName());
		console.log(player.getPhoto("medium"));

		myGameInstance.SendMessage("Yandex", "SetPlayerName", player.getName());
		myGameInstance.SendMessage("Yandex", "SetAvatar", player.getPhoto("medium"));
  	},
	
	RateGameExtern: function() {
    	ysdk.feedback.canReview()
			.then(({ value, reason }) => {
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
    	var dataString = UTF8ToString(data);
		var myObj = JSON.parse(dataString);
		player.setData(myObj);
  	},
	
	LoadExtern: function() {
		if (player == undefined)
		{
			console.log("LoadExtern: player is undefined");
			return;
		}
	
    	player.getData().then(_data => {
			const myJSON = JSON.stringify(_data);
			myGameInstance.SendMessage("Progress", "SetPlayerInfo", myJSON);
		});
  	},
	
	SetToLeaderboard: function(value) {
		ysdk.getLeaderboards()
			.then(lb => {
				lb.setLeaderboardScore('Score', value);
			});
	},
	
	GetLang: function() {
		var lang = ysdk.environment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang, buffer, bufferSize);
		return buffer;
	},
	
	ResurrectExtern: function() {
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