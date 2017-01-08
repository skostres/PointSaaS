/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var ProjectsCtrl = (function () {
    function ProjectsCtrl($scope, $state, SystemSrv) {
        $scope.model = this;
        //$scope.state = $state;
        SystemSrv.setCurrentPage($state.current.name);
        this.message = "Hello World!! I am a projects controller";
    }
    ;
    return ProjectsCtrl;
}());
//# sourceMappingURL=ProjectsCtrl.js.map