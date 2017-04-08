/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var ReportCtrl = (function () {
    function ReportCtrl($scope, $state, SignalRSrv, ModalService, growl, NgTableParams) {
        $scope.report = {};
        this.currentToggleLevel = Level.INTRODUCTION;
        $scope.currentToggleLevel = this.currentToggleLevel;
        // SignalR Setup
        console.log('trying to connect to service');
        var connection = SignalRSrv.connection;
        var reportHub = connection.createHubProxy('reportHub');
        reportHub.on('reportPush', function (data) {
            var report = new Array();
            data.teamList.map(function (value) {
                var school = data.schoolList[value.ID];
                var stuList = data.studentList[value.ID];
                var teamSub = data.submissionList[value.ID];
                report.push(new ReportRecord(value, school, stuList, teamSub));
            }.bind(this));
            $scope.report = report;
            $scope.tableParams = new NgTableParams({ sorting: { score: "desc" } }, { dataset: $scope.report });
            $scope.$apply();
        }.bind(this));
        $scope.toggleLevel = function () {
            if (this.currentToggleLevel == Level.ADVANCED.valueOf()) {
                this.currentToggleLevel = Level.INTRODUCTION;
            }
            else {
                this.currentToggleLevel = Level.ADVANCED;
            }
            $scope.currentToggleLevel = this.currentToggleLevel;
            reportHub.invoke("PollReportByLevel", $scope.currentUser, this.currentToggleLevel);
        }.bind(this);
        //Subscribe(UserDTO usr)
        connection.start().done(function () {
            console.log('Connection established!');
            reportHub.invoke("Subscribe", $scope.currentUser);
            reportHub.invoke("PollReportByLevel", $scope.currentUser, this.currentToggleLevel);
        }.bind(this));
    }
    ;
    ReportCtrl.prototype.base64ToHex = function (str) {
        for (var i = 0, bin = atob(str.replace(/[ \r\n]+$/, "")), hex = []; i < bin.length; ++i) {
            var tmp = bin.charCodeAt(i).toString(16);
            if (tmp.length === 1)
                tmp = "0" + tmp;
            hex[hex.length] = tmp;
        }
        return hex.join(" ");
    };
    // Data is Base64 string
    ReportCtrl.prototype.downloadFile = function (name, data) {
        var byteCharacters = atob(data);
        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        var file = new Blob([byteArray], {
            type: 'application/zip'
        });
        //trick to download store a file having its URL
        var fileURL = URL.createObjectURL(file);
        var a = document.createElement('a');
        a.href = fileURL;
        a.target = '_blank';
        a.download = name + '.zip';
        document.body.appendChild(a);
        a.click();
    };
    return ReportCtrl;
}());
