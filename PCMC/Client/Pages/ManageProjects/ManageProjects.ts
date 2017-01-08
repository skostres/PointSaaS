/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

class ManageProjectsCtrl {
    public message: string;
    constructor($scope, $state, SystemSrv: SystemSrv) {
        $scope.model = this;
        //$scope.state = $state;
        this.message = "Hello World!! I am a projects controller";
    };

}