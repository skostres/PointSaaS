/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />
var ProjectsCtrl = (function () {
    function ProjectsCtrl($scope, $route, SystemSrv) {
        $scope.model = this;
        SystemSrv.setCurrentPage($route.current.$$route.name);
        this.message = "Hello World!! I am a projects controller";
    }
    ;
    return ProjectsCtrl;
}());
//# sourceMappingURL=ProjectsCtrl.js.map