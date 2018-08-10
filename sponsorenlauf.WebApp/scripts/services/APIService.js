/**
 * Created by Arne on 19.10.2014.
 */
(function ()
{
    'use strict';

    angular
        .module('myapp')
        .factory("APIService", ['$http', '$routeParams', APIService]);

    function APIService($http, $routeParams)
    {
        var UrlToCall="http://192.168.0.101:8080/v1/mobile/";
		var Bookmarks= [];
		Activate();
		
		function Activate()
		{
			var bm = localStorage.getItem("Bookmarks");
			console.log("get",bm);
			if(bm != null)
				Bookmarks = JSON.parse(bm);	
		}
		

        return {
            GetDashboardData: GetDashboardData,
			GetToken : GetToken,
			IncreaseLaeufer : IncreaseLaeufer,
			DecreaseLaeufer : DecreaseLaeufer,
			Bookmark : Bookmark,
			IsBookmarked : IsBookmarked
        };

        ////////////////////////////////////////

        function GetDashboardData()
        {
			var token = $routeParams.token;
            return $http.get(UrlToCall + "laeuferList/")
                .then(function (response)
                {
                    return OrderResponse(response.data);
                });
        };
		
		function IncreaseLaeufer(lauefer)
        {
			var token = $routeParams.token;
            return $http.get(UrlToCall + "rundeIncrease/"+lauefer.Id+"/"+(lauefer.Runden+1)+"/")
                .then(function (response)
                {
                    return OrderResponse(response.data);
                });
        };
		
		function DecreaseLaeufer(lauefer)
        {
			var token = $routeParams.token;
            return $http.get(UrlToCall + "rundeIncrease/"+lauefer.Id+"/"+(lauefer.Runden-1)+"/")
                .then(function (response)
                {
                    return OrderResponse(response.data);
                });
        };
		
		function OrderResponse(data)
		{
			var sortedList = _.sortBy(data, e => !_.include(Bookmarks, e.Id));
			return sortedList;
		}
		
		function Bookmark(lauefer)
        {
			if (!_.include(Bookmarks, lauefer.Id))
			{
				Bookmarks.push(lauefer.Id);				
			}
			else
			{
				_.pull(Bookmarks,lauefer.Id );
			}
			localStorage.setItem("Bookmarks", JSON.stringify(Bookmarks));
			GetDashboardData();
        };
		
		function IsBookmarked(lauefer)
        {
			if (!_.include(Bookmarks, lauefer.Id))
			{
				return false;
			}
			return true;
        };
		
		function GetToken()
        {
			return $routeParams.token;
        };

       

    };

}());
