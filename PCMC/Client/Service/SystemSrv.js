/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />
var SystemSrv = (function () {
    function SystemSrv() {
    }
    SystemSrv.prototype.SystemSrv = function () { };
    SystemSrv.prototype.setCurrentPage = function (page) {
        this.currentPage = page;
    };
    SystemSrv.prototype.getCurrentPage = function () {
        return this.currentPage;
    };
    return SystemSrv;
}());
//# sourceMappingURL=SystemSrv.js.map