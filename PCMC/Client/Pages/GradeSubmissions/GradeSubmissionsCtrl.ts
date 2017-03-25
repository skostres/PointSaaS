/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

class GradeSubmissionsCtrl {

    constructor($scope, $state, SignalRSrv, ModalService, growl, NgTableParams) {
        //$scope.state = $state;
        $scope.projects = {};
        $scope.projects.list = new Array<TeamSubmission>();
        $scope.score = 0;

        // SignalR Setup
        console.log('trying to connect to service')
        var connection = SignalRSrv.connection;
        var projectsHub = connection.createHubProxy('projectsHub');
        var gradeSubmissionsHub = connection.createHubProxy('gradeSubmissionsHub');

        projectsHub.on('notifyChange', function (proj: Project, msg: string, type: MsgType) {
            switch (type) {
                case MsgType.SUCCESS: growl.success(msg); break;
                case MsgType.INFORMATION: growl.info(msg); break;
                case MsgType.WARNING: growl.warning(msg); break;
                case MsgType.ERROR: growl.error(msg); break;
            }
        }.bind(this));

       gradeSubmissionsHub.on('submissionTeamPoll', function (data: Array<TeamSubmission>) {
            // Map each element to corresponding project
           $scope.projects.submissions = data;

            $scope.tableParams = new NgTableParams({}, { dataset: $scope.projects.submissions });
            $scope.$apply();
        }.bind(this));

       gradeSubmissionsHub.on('updatedSubmission', function (data: TeamSubmission) {
           if ($scope.projects.submissions != null) {
               var found = false;
               $scope.projects.submissions.map(function (value: TeamSubmission, index: number, arr: TeamSubmission[]) {
                   if (value.ID == data.ID) {
                        // Update relevant data
                       value.RawZipSolution = data.RawZipSolution;
                       value.Score = data.Score;
                       value.Project = data.Project;
                       value.GraderComment = data.GraderComment;
                       found = true;
                   }
               });

               if (!found) {
                   $scope.projects.submissions.push(data);
               }
               $scope.tableParams = new NgTableParams({}, { dataset: $scope.projects.submissions });
               $scope.$apply();
           }

       }.bind(this));

        //Subscribe(UserDTO usr)
        connection.start().done(function () {
            console.log('Connection established!');
            projectsHub.invoke("Subscribe", $scope.currentUser);
            gradeSubmissionsHub.invoke("Subscribe", $scope.currentUser);
            gradeSubmissionsHub.invoke("submissionTeamPoll", $scope.currentUser);
        });

        this.addSubmissionModal = function (sub: TeamSubmission) {
            this.showAModal(sub,
                "Client/Pages/GradeSubmissions/GradeSubmissionModal.html",
                "GenericYesNoModalCtrl", function (result) {
                    if (!result.cancel) {
                        // Any file uploads?

                        // Is the score within a valid value, -1 special value indicator for ungraded.
                        if (sub.Score != -1 && sub.Score < 0 && sub.Score <= sub.Project.MaxScore) {
                            growl.warning("Please enter a score from -1 or to the MaxScore");
                        } else {
                            sub.Score = result.param1;
                            gradeSubmissionsHub.invoke('GradeSubmission',$scope.currentUser, sub);
                        }
                    }
                });
        };

        this.showAModal = function (project: TeamSubmission, template: string, controller: string, callback) {
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

    };

    // MODAL METHODS BELOW
    public showAModal: any;
    public addSubmissionModal: any;
    public base64ToHex(str: string) {
        for (var i = 0, bin = atob(str.replace(/[ \r\n]+$/, "")), hex = []; i < bin.length; ++i) {
            var tmp = bin.charCodeAt(i).toString(16);
            if (tmp.length === 1) tmp = "0" + tmp;
            hex[hex.length] = tmp;
        }
        return hex.join(" ");
    }

    // Data is Base64 string
    public downloadFile(name, data) {
        var byteCharacters = atob(data);
        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers)

        var file = new Blob([byteArray], {
            type: 'application/zip'
        });
        //trick to download store a file having its URL
        var fileURL = URL.createObjectURL(file);
        var a: any = document.createElement('a');
        a.href = fileURL;
        a.target = '_blank';
        a.download = name + '.zip';
        document.body.appendChild(a);
        a.click();
    }

}