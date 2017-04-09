/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var InstancesCtrl = (function () {
    function InstancesCtrl($scope, $state, SignalRSrv, ModalService, growl, NgTableParams) {
        // SignalR Setup
        console.log('trying to connect to service');
        var connection = SignalRSrv.connection;
        var dashboardHub = connection.createHubProxy('CommunicationHub');
        dashboardHub.on('updateInstanceList', function (data) {
            $scope.myInstances = data;
            $scope.tableParams = new NgTableParams({ sorting: { score: "desc" } }, { dataset: $scope.myInstances });
            $scope.$apply();
        });
        // Server tells client to request data refresh due to complex data change
        dashboardHub.on('triggerUpdate', function () {
            dashboardHub.invoke("PollInstanceList", $scope.currentUser);
        }.bind(this));
        // SignalR Start session
        connection.start().done(function () {
            console.log('Connection established!');
            dashboardHub.invoke("Subscribe", $scope.currentUser);
            dashboardHub.invoke("PollInstanceList", $scope.currentUser);
        });
    }
    ;
    return InstancesCtrl;
}());
//# sourceMappingURL=InstancesCtrl.js.map