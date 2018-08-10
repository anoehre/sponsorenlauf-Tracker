		(function () {
    'use strict';

    angular
        .module('myapp.dashboard')
        .controller('dashboardController', dashboardController);

	dashboardController.$inject = ['APIService','$interval','$scope'];

    function dashboardController(APIService, $interval, $scope) {

        var vm = this;		
		
		var connection = $.hubConnection('http://192.168.0.101:8080');
		var chat = connection.createHubProxy('DashboardHub');
		 
        chat.on('hello', function (message, callbackId) {
                     Activate();
        });

        connection.logging = false;
        connection.start()
                    .done(function () {

                        chat.invoke('Register', APIService.GetToken(), chat.connection.id);
                        
                    })
                    
                connection.reconnected(function () {
                    chat.invoke('Register', APIService.GetToken(), chat.connection.id);
                });
				
		
		vm.Increase = Increase;
		vm.Decrease = Decrease;
		vm.Bookmark = Bookmark;
		vm.IsBookmarked = IsBookmarked;
	
        Activate();

        /////////////////////////////

        function Activate() {			
            APIService.GetDashboardData()
                .then(OnDashboardDataComplete);
		};
		
		
	
		function OnDashboardDataComplete(data)
        {
			if (data=="") 
			{
				vm.DashboardData = "";
				return;
			}
            vm.DashboardData = data;
        };
		
		function Increase(lauefer)
		{
			APIService.IncreaseLaeufer(lauefer)
			 .then(OnDashboardDataComplete);
		};
		
		function Decrease(lauefer)
		{
			APIService.DecreaseLaeufer(lauefer)
			 .then(OnDashboardDataComplete);
		};
		
		function Bookmark(lauefer)
		{
			APIService.Bookmark(lauefer);
			Activate();
		};
		
		function IsBookmarked(lauefer)
		{
			return APIService.IsBookmarked(lauefer);
		};
		
		
	}

}());


