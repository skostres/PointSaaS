/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />

class DashboardCtrl {
    public message: string;
    constructor($scope, $route, SystemSrv: SystemSrv) {
        $scope.model = this;
        SystemSrv.setCurrentPage($route.current.$$route.name);
        this.message = "Hello World!! I am a dashboard controller";
    };

}