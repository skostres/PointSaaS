/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />

class NavigationCtrl {
    public Sys: SystemSrv;
    public growl: any;
    constructor($scope, $http, $rootScope, SystemSrv: SystemSrv, growl) {
        this.Sys = SystemSrv;
        $scope.vm = this;
        // Generate level options
        $rootScope.levels = {};
        this.growl = growl;
        $rootScope.checkExtension =
            function (model: AddInstanceModel) {

            var toSend = new ExtensionURLModel();
            toSend.Extension = model.URLExtension;
            toSend.IsValid = false;
            return $http
                .post('api/Auth/ExtensionCheck', toSend)
                .then(function (res) {
                    model.IsValidExtension = res.data.IsValid;
                    return res;
                }.bind(this))
                .catch(function (response) {
                    
                }.bind(this))
        }.bind(this);
    };

}