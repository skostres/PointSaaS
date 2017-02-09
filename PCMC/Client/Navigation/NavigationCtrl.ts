/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />

class NavigationCtrl {
    public Sys: SystemSrv;
    constructor($scope, $rootScope, SystemSrv: SystemSrv) {
        this.Sys = SystemSrv;
        $scope.vm = this;
        // Generate level options
        $rootScope.levels = {};
        for (var enumMember in Level) {
            var isValueProperty = parseInt(enumMember, 10) >= 0
            if (isValueProperty) {
                $rootScope.levels[enumMember] = { "ID": enumMember, "Name": Level[enumMember] };
            }
        }
    };

}