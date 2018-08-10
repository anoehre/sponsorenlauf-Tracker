(function ()
{
    'use strict';

    var myapp = angular.module('myapp');

    var chartDirective = function ()
    {
        return {
            restrict: 'E',
            replace:  true,
            template: '<div></div>',
            scope:    {
                config: '='
            },
            link:     function (scope, element, attrs)
            {
                var chart;
                var process = function ()
                {
                    var defaultOptions = {
                        credits: {enabled: false},
                        chart:   {zoomType: 'xy', type: 'line', renderTo: element[0]},
                        title:   {text: 'Chart'},
                        legend:  {
                            layout:        'horizontal',
                            align:         'center',
                            verticalAlign: 'bottom'
                        }
                    };
                    var config = angular.extend(defaultOptions, scope.config);
                    chart = new Highcharts.Chart(config);
                };
                process();
                scope.$watch("config.series", function (loading)
                {
                    process();
                });
                scope.$watch("config.loading", function (loading)
                {
                    if (!chart)
                    {
                        return;
                    }
                    if (loading)
                    {
                        chart.showLoading();
                    }
                    else
                    {
                        chart.hideLoading();
                    }
                });
            }
        };
    };
    myapp.directive('chart', chartDirective);

}());