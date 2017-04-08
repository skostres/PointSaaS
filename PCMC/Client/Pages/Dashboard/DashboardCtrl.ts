/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

class DashboardCtrl {

    constructor($scope: any, $state: any, SignalRSrv: any, ModalService: any, growl: any) {

        // SignalR Setup
        console.log('trying to connect to service')
        var connection = SignalRSrv.connection;
        var dashboardHub = connection.createHubProxy('dashboardHub');
        var projectsHub = connection.createHubProxy('projectsHub');

        projectsHub.on('notifyChange', function (proj: Project, msg: string, type: MsgType) {
            switch (type) {
                case MsgType.SUCCESS: growl.success(msg); break;
                case MsgType.INFORMATION: growl.info(msg); break;
                case MsgType.WARNING: growl.warning(msg); break;
                case MsgType.ERROR: growl.error(msg); break;
            }
        }.bind(this));

        // Initial values
        $scope.numberOfProjects = 0;    // Admin View
        $scope.numberOfJudges = 0;      // Admin View
        $scope.numberOfStudents = 0;    // Admin View
        $scope.numberOfTeams = 0;       // Admin View
        $scope.numberOfSubmissionsAwaitingGrades = 0;   // Admin / Judge view
        

        $scope.numberOfProjectsAwaitingSubmissions = 0; //Participant View Only

        // Admin relevant data
        if ($scope.currentUser.UserRole == Role.Admin) {
            dashboardHub.on('updateNumberOfProjects', function (data: number) {
                $scope.numberOfProjects = data;
                $scope.$apply();
            });

            dashboardHub.on('updateNumberOfJudges', function (data: number) {
                $scope.numberOfJudges = data;
                $scope.$apply();
            });

            dashboardHub.on('updateNumberOfStudents', function (data: number) {
                $scope.numberOfStudents = data;
                $scope.$apply();
            });

            dashboardHub.on('updateNumberOfTeams', function (data: number) {
                $scope.numberOfTeams = data;
                $scope.$apply();
            });
        }

        // Admn / Judge relevant data
        if ($scope.currentUser.UserRole == Role.Admin || $scope.currentUser.UserRole == Role.User) {
            dashboardHub.on('updateNumberOfSubmissionsAwaitingGrades', function (data: number) {
                $scope.numberOfSubmissionsAwaitingGrades = data;
                $scope.$apply();
            });
        }

        // Server tells client to request data refresh due to complex data change
        dashboardHub.on('triggerUpdate', function () {
            dashboardHub.invoke("PollInitialData", $scope.currentUser);
        });

        // SignalR Start session
        connection.start().done(function () {
            console.log('Connection established!');
            projectsHub.invoke("Subscribe", $scope.currentUser);
            dashboardHub.invoke("Subscribe", $scope.currentUser);
            dashboardHub.invoke("PollInitialData", $scope.currentUser);
        });


    };

}