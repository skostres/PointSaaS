/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
var SubmitProjectsCtrl = (function () {
    function SubmitProjectsCtrl($scope, $state, SignalRSrv, ModalService, growl, NgTableParams) {
        //$scope.state = $state;
        $scope.projects = {};
        $scope.projects.list = new Array();
        // SignalR Setup
        console.log('trying to connect to service');
        var connection = SignalRSrv.connection;
        var projectsHub = connection.createHubProxy('projectsHub');
        var submitProjectsHub = connection.createHubProxy('submitProjectsHub');
        projectsHub.on('projectListPoll', function (data) {
            // alert(data);
            // ProjectsCtrl.downloadFile(data[0].Name+'Judge-Instructions', data[0].RawZipFileJudges);
            $scope.projects.list = data;
            submitProjectsHub.invoke("pollTeamSubmissions", $scope.currentUser);
            //ngAlertsMngr.add({ msg: "Project List Changed/Updated", type:'warning'});
            //this.projectList = data;
        }.bind(this));
        projectsHub.on('notifyChange', function (proj, msg, type) {
            switch (type) {
                case MsgType.SUCCESS:
                    growl.success(msg);
                    break;
                case MsgType.INFORMATION:
                    growl.info(msg);
                    break;
                case MsgType.WARNING:
                    growl.warning(msg);
                    break;
                case MsgType.ERROR:
                    growl.error(msg);
                    break;
            }
        }.bind(this));
        submitProjectsHub.on('submissionTeamPoll', function (data) {
            // Map each element to corresponding project
            $scope.projects.submissions = new Array();
            // Get a hashmap of project_id->TeamSubmission
            var mapping = {};
            data.map(function (value) {
                mapping[value.Project.ID] = value;
            }.bind(this));
            // Fill array of team submissions
            $scope.projects.list.map(function (proj, index) {
                // True if key doesn't exists
                if (!(proj.ID in mapping)) {
                    $scope.projects.submissions.push(new TeamSubmission(-1, null, proj, null, -1));
                }
                else {
                    $scope.projects.submissions.push(mapping[proj.ID]);
                }
            }.bind(this));
            $scope.tableParams = new NgTableParams({}, { dataset: $scope.projects.submissions });
            $scope.$apply();
        }.bind(this));
        //Subscribe(UserDTO usr)
        connection.start().done(function () {
            console.log('Connection established!');
            projectsHub.invoke("Subscribe", $scope.currentUser);
            submitProjectsHub.invoke("Subscribe", $scope.currentUser);
            projectsHub.invoke("PollProjectList", $scope.currentUser);
        });
        this.addSubmissionModal = function (proj) {
            this.showAModal(proj, "Client/Pages/SubmitProjects/AddSubmissionModal.html", "GenericYesNoModalCtrl", function (result) {
                if (!result.cancel) {
                    // Any file uploads?
                    // Upload Zip
                    if (result.uploadme1.src != "") {
                        submitProjectsHub.invoke("addSubmission", $scope.currentUser, new TeamSubmission(0, result.uploadme1.src.data.split(",")[1], proj, null, 0));
                    }
                    else {
                        growl.warning("You must first make a submission!");
                    }
                }
            });
        };
        this.showAModal = function (project, template, controller, callback) {
            // Just provide a template url, a controller and call 'showModal'.
            ModalService.showModal({
                templateUrl: template,
                controller: controller,
                inputs: {
                    model: project
                }
            }).then(function (modal) {
                // The modal object has the element built, if this is a bootstrap modal
                // you can call 'modal' to show it, if it's a custom modal just show or hide
                // it as you need to.
                modal.element.modal();
                modal.close.then(function (result) {
                    callback(result);
                }.bind(this));
            }.bind(this));
        }.bind(this);
    }
    ;
    SubmitProjectsCtrl.prototype.base64ToHex = function (str) {
        for (var i = 0, bin = atob(str.replace(/[ \r\n]+$/, "")), hex = []; i < bin.length; ++i) {
            var tmp = bin.charCodeAt(i).toString(16);
            if (tmp.length === 1)
                tmp = "0" + tmp;
            hex[hex.length] = tmp;
        }
        return hex.join(" ");
    };
    // Data is Base64 string
    SubmitProjectsCtrl.prototype.downloadFile = function (name, data) {
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
    return SubmitProjectsCtrl;
}());
