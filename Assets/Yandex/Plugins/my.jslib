mergeInto(LibraryManager.library, {

	FetchPlayerDataExtern: function() {
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
	
});