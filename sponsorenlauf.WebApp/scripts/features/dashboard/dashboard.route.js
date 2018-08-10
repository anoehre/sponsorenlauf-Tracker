/**
 * Created by anoehre on 04.11.2014.
 */
(function ()
{
    "use strict";

    angular
        .module('myapp.dashboard')
        .config(function ($routeProvider)
        {
            $routeProvider
                .when("/dashboard", {
                    templateUrl: "scripts/features/dashboard/dashboard.html",
                    controller:  "dashboardController",
                    controllerAs: "vm",
                    title:       "dashboardController"
                });
        });
}());
