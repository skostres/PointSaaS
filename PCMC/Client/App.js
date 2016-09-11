var PCMCApp = angular.module("mainApp", ['ngRoute', 'ngAnimate'])
    .config(function ($routeProvider) {
        $routeProvider
        // route for the dashboard page
        .when('/', {
            templateUrl: 'Client/Pages/Dashboard/Dashboard.html',
            controller: 'DashboardCtrl',
            name: 'Dashboard'
        })
         .when('/Projects', {
             templateUrl: 'Client/Pages/Projects/Projects.html',
             controller: 'ProjectsCtrl',
             name: 'Projects'
         })
        .otherwise({
            redirectTo: '/'
        });
    })
    .service('SystemSrv', SystemSrv)
    .controller("NavigationCtlr", NavigationCtrl)
    .controller("DashboardCtrl", DashboardCtrl)
    .controller("ProjectsCtrl", ProjectsCtrl)
    .controller("NotificationsCtrl", NotificationsCtrl)
    .factory('jQuery', [
            '$window',
            function ($window) {
                return $window.jQuery;
            }
    ])
    .directive('draggable', function ($document) {
        return function (scope, element, attr) {
            var startX = 0, startY = 0, x = 0, y = 0;
            element.css({
                position: 'relative',
                cursor: 'pointer',
                display: 'block',
            });
            element.on('mousedown', function (event) {
                // Prevent default dragging of selected content
                event.preventDefault();
                startX = event.screenX - x;
                startY = event.screenY - y;
                $document.on('mousemove', mousemove);
                $document.on('mouseup', mouseup);
            });

            function mousemove(event) {
                y = event.screenY - startY;
                x = event.screenX - startX;
                element.css({
                    top: y + 'px',
                    left: x + 'px'
                });
            }

            function mouseup() {
                $document.off('mousemove', mousemove);
                $document.off('mouseup', mouseup);
            }
        };
    });