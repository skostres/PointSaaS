/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />

class NavigationCtrl {
    public Sys: SystemSrv;
    constructor($scope, SystemSrv: SystemSrv) {
        this.Sys = SystemSrv;
        $scope.vm = this;
    };

}