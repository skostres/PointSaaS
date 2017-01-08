/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var DashboardCtrl = (function () {
    function DashboardCtrl($scope, $state, SystemSrv) {
        $scope.model = this;
        //$scope.$apply()
        SystemSrv.setCurrentPage($state.current.name);
        this.message = "Hello World!! I am a dashboard controller";
    }
    ;
    return DashboardCtrl;
}());
//# sourceMappingURL=DashboardCtrl.js.map