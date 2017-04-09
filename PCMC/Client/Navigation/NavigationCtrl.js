/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />
var NavigationCtrl = (function () {
    function NavigationCtrl($scope, $http, $rootScope, SystemSrv, growl) {
        this.Sys = SystemSrv;
        $scope.vm = this;
        // Generate level options
        $rootScope.levels = {};
        this.growl = growl;
        $rootScope.checkExtension =
            function (model) {
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
                }.bind(this));
            }.bind(this);
    }
    ;
    return NavigationCtrl;
}());
//# sourceMappingURL=NavigationCtrl.js.map