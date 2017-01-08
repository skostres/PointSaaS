/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var GradeSubmissionsCtrl = (function () {
    function GradeSubmissionsCtrl($scope, $state, SystemSrv) {
        $scope.model = this;
        //$scope.state = $state;
        SystemSrv.setCurrentPage($state.current.name);
        this.message = "Hello World!! I am a projects controller";
    }
    ;
    return GradeSubmissionsCtrl;
}());
//# sourceMappingURL=GradeSubmissionsCtrl.js.map