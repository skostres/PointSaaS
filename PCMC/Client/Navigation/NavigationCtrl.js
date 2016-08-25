/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />
var NavigationCtrl = (function () {
    function NavigationCtrl($scope, SystemSrv) {
        this.Sys = SystemSrv;
        $scope.vm = this;
    }
    ;
    return NavigationCtrl;
}());
//# sourceMappingURL=NavigationCtrl.js.map