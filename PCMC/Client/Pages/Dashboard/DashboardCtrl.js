/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var DashboardCtrl = (function () {
    function DashboardCtrl($scope, $state, $http, SignalRSrv, ModalService, growl, NgTableParams) {
        // SignalR Setup
        console.log('trying to connect to service');
        var connection = SignalRSrv.connection;
        this.dashboardHub = connection.createHubProxy('CommunicationHub');
        this.scope = $scope;
        this.ModalService = ModalService;
        this.http = $http;
        this.growl = growl;
        $scope.checkExtension = function (extension) {
            alert("HERE");
            return this.http
                .post('api/Auth/ExtensionCheck', extension)
                .then(function (res) {
                return res;
            }.bind(this))
                .catch(function (response) {
                this.error("Extension In Use", { ttl: 10000 });
            }.bind(this));
        };
        this.dashboardHub.on('updateInstanceList', function (data) {
            $scope.myInstances = data;
            $scope.tableParams = new NgTableParams({ sorting: { score: "desc" } }, { dataset: $scope.myInstances });
            $scope.$apply();
        });
        // Server tells client to request data refresh due to complex data change
        this.dashboardHub.on('triggerUpdate', function () {
            this.dashboardHub.invoke("PollInstanceList", $scope.currentUser);
        }.bind(this));
        this.dashboardHub.on('notifyChange', function (msg, type) {
            switch (type) {
                case MsgType.SUCCESS:
                    growl.success(msg);
                    break;
                case MsgType.INFORMATION:
                    growl.info(msg);
                    break;
                case MsgType.WARNING:
                    growl.warning(msg);
                    break;
                case MsgType.ERROR:
                    growl.error(msg);
                    break;
            }
        }.bind(this));
        this.dashboardHub.on('updateServerLocationList', function (data) {
            $scope.serverLocations = data;
            $scope.$apply();
        }.bind(this));
        this.dashboardHub.on('updateInstanceTypeList', function (data) {
            $scope.instanceOptions = data;
            $scope.$apply();
        }.bind(this));
        // SignalR Start session
        connection.start().done(function () {
            console.log('Connection established!');
            this.dashboardHub.invoke("Subscribe", $scope.currentUser);
            this.dashboardHub.invoke("PollInstanceList", $scope.currentUser);
            this.dashboardHub.invoke("PollServerLocations", $scope.currentUser);
            this.dashboardHub.invoke("PollInstanceTypes", $scope.currentUser);
        }.bind(this));
    }
    ;
    DashboardCtrl.prototype.InstanceAction = function (instance, action) {
        switch (action) {
            case "stop":
                alert("Stop: Operation not yet implemented");
                break;
            case "resume":
                alert("resume: Operation not yet implemented");
                break;
            case "pause":
                alert("Pause: Operation not yet implemented");
                break;
            case "delete":
                alert("Delete: Operation not yet implemented");
                break;
            default:
                break;
        }
    };
    DashboardCtrl.prototype.addInstanceModal = function () {
        var addInstanceModel = new AddInstanceModel();
        addInstanceModel.ServerLocationOptions = this.scope.serverLocations;
        addInstanceModel.InstanceTypeOptions = this.scope.instanceOptions;
        addInstanceModel.IsValidExtension = false;
        this.showAModal(addInstanceModel, "Client/Pages/Dashboard/AddInstanceModal.html", "GenericYesNoModalCtrl", function (result) {
            if (!result.cancel) {
                result.model.ServerLocationOptions = null;
                result.model.InstanceTypeOptions = null;
                this.dashboardHub.invoke("RequestInstance", this.scope.currentUser, result.model);
            }
        }.bind(this));
    };
    DashboardCtrl.prototype.showAModal = function (model, template, controller, callback) {
        // Just provide a template url, a controller and call 'showModal'.
        this.ModalService.showModal({
            templateUrl: template,
            controller: controller,
            inputs: {
                model: model
            }
        }).then(function (modal) {
            // The modal object has the element built, if this is a bootstrap modal
            // you can call 'modal' to show it, if it's a custom modal just show or hide
            // it as you need to.
            modal.element.modal();
            modal.close.then(function (result) {
                callback(result);
            }.bind(this));
        }.bind(this));
    };
    return DashboardCtrl;
}());
//# sourceMappingURL=DashboardCtrl.js.map