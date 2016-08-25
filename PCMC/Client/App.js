var PCMCApp = angular.module("mainApp", ['ngRoute'])
    .config(function($routeProvider) {
        $routeProvider
        // route for the dashboard page
        .when('/', {
            templateUrl: 'Client/Dashboard/Dashboard.html',
            controller: 'DashboardCtrl',
            name: 'Dashboard'
        })
         .when('/Projects', {
            templateUrl: 'Client/Projects/Projects.html',
            controller: 'ProjectsCtrl',
            name: 'Projects'
         })
        .otherwise({
            redirectTo: '/'
        });
    })
    .controller("NavigationCtlr", NavigationCtrl)
    .controller("DashboardCtrl", DashboardCtrl)
    .controller("ProjectsCtrl", ProjectsCtrl)
    .service('SystemSrv', SystemSrv)