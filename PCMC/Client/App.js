var PCMCApp = angular.module("mainApp", ['ui.router', 'ngAnimate', 'SignalR', 'ng.epoch', 'n3-pie-chart', 'angularModalService', "ng-file-model", 'ui.bootstrap', 'angular-growl', 'ngTable', 'ceibo.components.table.export'])
    .constant('AUTH_EVENTS', {
        loginSuccess: 'auth-login-success',
        loginFailed: 'auth-login-failed',
        logoutSuccess: 'auth-logout-success',
        sessionTimeout: 'auth-session-timeout',
        notAuthenticated: 'auth-not-authenticated',
        notAuthorized: 'auth-not-authorized'
    })
    .constant('USER_ROLES', {
        admin: "Admin",
        judge: "Judge",
        participant: "Participant",
        guest: "Guest",
        all: "*"
    });


PCMCApp.config(function ($stateProvider, $urlRouterProvider, USER_ROLES) {
    $stateProvider.state('Login', {
        url: '/',
        controller: function ($scope, $rootScope, $state, AUTH_EVENTS) {
            if ($scope.currentUser == null || $scope.currentUser.ID == null)
                $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);
            else
                $state.go("Dashboard");
        },
        data: {
            authorizedRoles: [USER_ROLES.all]
        }
    });
    $stateProvider.state('Dashboard', {
        url: '/Dashboard',
        templateUrl: 'Client/Pages/Dashboard/Dashboard.html',
        controller: "DashboardCtrl",
        data: {
            authorizedRoles: [USER_ROLES.admin, USER_ROLES.judge, USER_ROLES.participant]
        }
    });

    $stateProvider.state('Performance', {
        url: '/Performance',
        templateUrl: 'Client/Pages/PerformanceDataController/PerformanceDataController.html',
        controller: "PerformanceDataController",
        data: {
            authorizedRoles: [USER_ROLES.admin]
        }
    });

    $stateProvider.state('Report', {
        url: '/Report',
        templateUrl: 'Client/Pages/Report/Report.html',
        controller: "ReportCtrl",
        data: {
            authorizedRoles: [USER_ROLES.admin]
        }
    });

    $stateProvider.state('Projects', {
        url: '/Projects',
        templateUrl: 'Client/Pages/Projects/Projects.html',
        controller: "ProjectsCtrl",
        data: {
            authorizedRoles: [USER_ROLES.admin, USER_ROLES.judge]
        }
    });


    $stateProvider.state('ManageProjects', {
        url: '/ManageProjects',
        templateUrl: 'Client/Pages/ManageProjects/ManageProjects.html',
        controller: "ManageProjectsCtrl",
        data: {
            authorizedRoles: [USER_ROLES.admin, USER_ROLES.judge]
        }
    });

    $stateProvider.state('GradeSubmissions', {
        url: '/GradeSubmissions',
        templateUrl: 'Client/Pages/GradeSubmissions/GradeSubmissions.html',
        controller: "GradeSubmissionsCtrl",
        data: {
            authorizedRoles: [USER_ROLES.admin, USER_ROLES.judge]
        }
    })

    $stateProvider.state('SubmitProjects', {
        url: '/SubmitProjects',
        templateUrl: 'Client/Pages/SubmitProjects/SubmitProjects.html',
        controller: "SubmitProjectsCtrl",
        data: {
            authorizedRoles: [USER_ROLES.participant]
        }
    })

        .state('Logout', {
            url: '/Logout',
            controller: function ($scope, $state, $rootScope, AUTH_EVENTS) {
                if ($scope.currentUser != null)
                    $scope.currentUser.destroy()

                $rootScope.$broadcast(AUTH_EVENTS.logoutSuccess);
                $state.go("Login");
            },
            data: {
                authorizedRoles: [USER_ROLES.all]
            }
        });

    $urlRouterProvider.otherwise(function ($injector, $location) {
        var $state = $injector.get("$state");
        $state.go("Login");
    });
    /*
    $stateProvider.state('Projects'), {
        url: '/Projects',
        templateUrl: 'Client/Pages/Projects/Projects.html',
        data: {
            authorizedRoles: [USER_ROLES.admin, USER_ROLES.judge]
        }
    }
    */

})
	.config(function ($httpProvider) {
	    $httpProvider.interceptors.push([
          '$injector',
          function ($injector) {
              return $injector.get('AuthInterceptor');
          }
	    ]);
	})
    .factory('AuthInterceptor', function ($rootScope, $q,
                                          AUTH_EVENTS) {
        return {
            responseError: function (response) {
                $rootScope.$broadcast({
                    401: AUTH_EVENTS.notAuthenticated,
                    403: AUTH_EVENTS.notAuthorized,
                    419: AUTH_EVENTS.sessionTimeout,
                    440: AUTH_EVENTS.sessionTimeout
                }[response.status], response);
                return $q.reject(response);
            }
        };
    })
    .service('SystemSrv', SystemSrv)

    .factory('jQuery', [
            '$window',
            function ($window) {
                return $window.jQuery;
            }
    ])
    .factory('AuthService', AuthService)

    // Modals
    .controller("GenericYesNoModalCtrl", GenericYesNoModalCtrl)
    .controller("NavigationCtlr", NavigationCtrl)
    .controller("DashboardCtrl", DashboardCtrl)
    .controller("ProjectsCtrl", ProjectsCtrl)
    .controller("NotificationsCtrl", NotificationsCtrl)
    .controller("GradeSubmissionsCtrl", GradeSubmissionsCtrl)
    .controller("ManageProjectsCtrl", ManageProjectsCtrl)
    .controller("SubmitProjectsCtrl", SubmitProjectsCtrl)
    .controller("ReportCtrl", ReportCtrl)
    .controller('AuthCtrl', function ($scope,
                                      USER_ROLES,
                                      AuthService) {
        $scope.currentUser = null;
        $scope.userRoles = USER_ROLES;
        $scope.isAuthorized = AuthService.isAuthorized;
        $scope.isLoginPage = true;

        $scope.setCurrentUser = function (user) {
            $scope.currentUser = user;
            $scope.isLoginPage = false;
        };
    })
    .controller("LoginCtrl", LoginCtrl)
    .directive('loginDialog', function (AUTH_EVENTS) {
        return {
            restrict: 'A',
            template: '<div ng-if="visible" ng-include="\'Client/Pages/Login/Login.html\'">',
            link: function (scope) {
                var showDialog = function () {
                    scope.visible = true;
                };

                var hideDialog = function () {
                    scope.visible = false;
                };

                scope.visible = false;
                scope.$on(AUTH_EVENTS.notAuthenticated, showDialog);
                scope.$on(AUTH_EVENTS.sessionTimeout, showDialog)
                scope.$on(AUTH_EVENTS.logoutSuccess, showDialog)
                scope.$on(AUTH_EVENTS.loginSuccess, hideDialog)
            }
        };
    })
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
    })

/* ALERTS CONFIG */
.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(5000);
    growlProvider.globalPosition('bottom-right');
}])
/********************************************************
SIGNAL R SETUP
*********************************************************/
    .value('backendServerUrl', '/signalr')
    .factory('SignalRSrv', ['$rootScope', 'backendServerUrl',
  function ($rootScope, backendServerUrl) {
      signalr = {}

      signalr.connection = $.hubConnection(backendServerUrl, { logging: true });
      return signalr;
  }])
    .controller('PerformanceDataController', ['$scope', 'backendServerUrl', 'SignalRSrv',
  function ($scope, backendServerUrl, SignalRSrv) {
      console.log('trying to connect to service')
      var connection = SignalRSrv.connection;
      var performanceDataHub = connection.createHubProxy('performanceHub');


      //Placeholders until data is pushed.
      $scope.currentRamNumber = 68;

      $scope.realtimeArea = [
          { label: 'Layer 1', values: [] },
          { label: 'Layer 2', values: [] },
          { label: 'Layer 3', values: [] },
          { label: 'Layer 4', values: [] },
          { label: 'Layer 5', values: [] },
          { label: 'Layer 6', values: [] }
      ];

      $scope.options = { thickness: 10, mode: "gauge", total: 100 };
      $scope.data = [
          { label: "CPU", value: 78, color: "#d62728", suffix: "%" }
      ];

      console.log('connected to service')
      //performanceDataHub.invoke("Heartbeat");
      performanceDataHub.on('numberOfUsers', function (data) {
          $scope.numberOfUsers = data;
      })
      performanceDataHub.on('broadcastPerformance', function (data) {
          var timestamp = ((new Date()).getTime() / 1000) | 0;
          var chartEntry = [];

          data.forEach(function (dataItem) {

              switch (dataItem.categoryName) {
                  case "Processor":
                      $scope.cpuData = dataItem.value;
                      chartEntry.push({ time: timestamp, y: dataItem.value });
                      $scope.data = [
                          { label: "CPU", value: dataItem.value, color: "#d62728", suffix: "%" }
                      ];
                      console.log($scope.data)
                      break;

                  case "Memory":
                      $scope.currentRamNumber = dataItem.value;
                      chartEntry.push({ time: timestamp, y: dataItem.value });
                      break;

                  case "Network In":
                      $scope.netInData = dataItem.value.toFixed(2);
                      chartEntry.push({ time: timestamp, y: dataItem.value });
                      break;

                  case "Network Out":
                      $scope.netOutData = dataItem.value.toFixed(2);
                      chartEntry.push({ time: timestamp, y: dataItem.value });
                      break;

                  case "Disk Read Bytes/Sec":
                      $scope.diskReaddData = dataItem.value.toFixed(3);
                      chartEntry.push({ time: timestamp, y: dataItem.value });
                      break;

                  case "Disk Write Bytes/Sec":
                      $scope.diskWriteData = dataItem.value.toFixed(3);
                      chartEntry.push({ time: timestamp, y: dataItem.value });
                      break;

                  default:
                      break;
                      //default code block
              }
              $scope.$apply()
          })


          $scope.realtimeAreaFeed = chartEntry;
      });
      connection.start().done(function () { console.log('Connection established!'); });

      $scope.areaAxes = ['left', 'right', 'bottom'];
  }
    ])
    .run(function ($rootScope, AUTH_EVENTS, AuthService, SignalRSrv, SystemSrv) {
        $rootScope.$on('$stateChangeStart', function (event, next) {


            var connection = SignalRSrv.connection
            if (connection && connection.state && connection.state !== 4 /* disconnected */) {
                console.log('signlar connection abort');
                connection.stop();
            }

            var authorizedRoles = next.data.authorizedRoles;
            if (!AuthService.isAuthorized(authorizedRoles)) {
                event.preventDefault();
                if (AuthService.isAuthenticated()) {
                    // user is not allowed
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthorized);
                    console.log("NOT AUTHORIZED")
                } else {
                    // user is not logged in
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);
                    console.log("NOT AUTHENTICATED")
                }
            } else {
                SystemSrv.setCurrentPage(next.name);
                $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
            }

        });

    });