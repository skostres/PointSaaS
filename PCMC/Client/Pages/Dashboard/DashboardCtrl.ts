/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

class DashboardCtrl {
    public message: string;
    constructor($scope, $state, SystemSrv: SystemSrv) {
        $scope.model = this;
        
        //$scope.$apply()
        SystemSrv.setCurrentPage($state.current.name);
        this.message = "Hello World!! I am a dashboard controller";
    };

}