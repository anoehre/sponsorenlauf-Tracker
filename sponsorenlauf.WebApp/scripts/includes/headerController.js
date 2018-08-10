(function ()
{
    angular
        .module('myapp')
        .controller('headerController', headerController);

    function headerController($scope, $location, $route)
    {
        $scope.isActive = function (viewLocation)
        {
            return viewLocation === $location.path();
        };

        $scope.CheckRoute = function (name)
        {
            for (var prop in $route.routes)
            {
                if ($route.routes.hasOwnProperty(prop))
                {
                    var route = $route.routes[prop];
                    if (!!route.title & route.title == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        };

        $scope.moduleExists = function (moduleName)
        {
            try
            {
                angular.module(moduleName);
            }
            catch (e)
            {
                if (/No module/.test(e) || (e.message.indexOf('$injector:nomod') > -1))
                {
                    return false;
                }
            }
            return true;
        }

    };

}());