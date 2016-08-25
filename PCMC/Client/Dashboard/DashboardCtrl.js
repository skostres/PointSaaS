/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />
var DashboardCtrl = (function () {
    function DashboardCtrl($scope, $route, SystemSrv) {
        $scope.model = this;
        SystemSrv.setCurrentPage($route.current.$$route.name);
        this.message = "Hello World!! I am a dashboard controller";
    }
    ;
    return DashboardCtrl;
}());
//# sourceMappingURL=DashboardCtrl.js.map