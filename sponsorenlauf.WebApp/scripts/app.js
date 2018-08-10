(function ()
{
    'use strict';

    var app = angular.module('myapp', [
        'ngRoute',
        'ui.bootstrap',
        'myapp.dashboard'
    ]);
	
    app.config(function ($routeProvider)
    {
        $routeProvider.otherwise({redirectTo: "/dashboard"});
    });


}());

