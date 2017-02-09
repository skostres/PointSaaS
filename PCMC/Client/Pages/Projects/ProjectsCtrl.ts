/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

class ProjectsCtrl {
    
    constructor($scope, $state, SignalRSrv, ModalService, growl) {
        //$scope.state = $state;
        $scope.projects = {};
        
        // SignalR Setup
        console.log('trying to connect to service')
        var connection = SignalRSrv.connection;
        var projectsHub = connection.createHubProxy('projectsHub');
        
        projectsHub.on('projectListPoll', function (data: Array<Project>) {

            // alert(data);
            // ProjectsCtrl.downloadFile(data[0].Name+'Judge-Instructions', data[0].RawZipFileJudges);
            $scope.projects.list = data;
            //ngAlertsMngr.add({ msg: "Project List Changed/Updated", type:'warning'});
            $scope.$apply();
            //this.projectList = data;
        }.bind(this));

        projectsHub.on('notifyChange', function (proj: Project, msg: string, type: MsgType) {
            switch (type) {
                case MsgType.SUCCESS: growl.success(msg); break;
                case MsgType.INFORMATION: growl.info(msg); break;
                case MsgType.WARNING: growl.warning(msg); break;
                case MsgType.ERROR: growl.error(msg); break;
            }
        }.bind(this));

        //Subscribe(UserDTO usr)
        connection.start().done(function () {
            console.log('Connection established!');
            projectsHub.invoke("Subscribe", $scope.currentUser);
            projectsHub.invoke("PollProjectList", $scope.currentUser);
        });

        this.editProjectModal = function (project: Project) {
            this.showAModal(project,
                "Client/Pages/Projects/EditProjectModal.html",
                "GenericYesNoModalCtrl", function (result) {
                if (!result.cancel) {
                    // Any file uploads?

                    // Upload Judge instructions
                    if (result.uploadme1.src != "") {
                        result.model.RawZipFileJudges = result.uploadme1.src.data.split(",")[1]
                    }

                    // Upload Participant Instructions
                    if (result.uploadme2.src != "") {
                        result.model.RawZipFileParticipants = result.uploadme2.src.data.split(",")[1];
                    }

                    projectsHub.invoke("UpdateProject", $scope.currentUser, result.model);
                }
            });
        }.bind(this);

        this.addProjectModal = function () {
            this.showAModal(new Project(-1, "", "", "New Project", 0, "Description", true, Level.INTRODUCTION),
                "Client/Pages/Projects/AddProjectModal.html",
                "GenericYesNoModalCtrl", function (result) {
                    if (!result.cancel) {
                        // Any file uploads?

                        // Upload Judge instructions
                        if (result.uploadme1.src != "" && result.uploadme2.src != "") {
                            result.model.RawZipFileJudges = result.uploadme1.src.data.split(",")[1];
                            result.model.RawZipFileParticipants = result.uploadme2.src.data.split(",")[1];
                            // Upload Participant Instructions

                            projectsHub.invoke("addProject", $scope.currentUser, result.model);
                        } else {
                            growl.warning("You must submit zip files for both instructor and participant");
                        }
                    }
                });
        };

        this.deleteProjectModal = function (project: Project) {
            this.showAModal(project,
                "Client/Pages/Projects/DeleteProjectModal.html",
                "GenericYesNoModalCtrl", function (result) {
                    if (!result.cancel) {
                        // Any file uploads?

                        // Upload Judge instructions
                        if (result.uploadme1.src != "") {
                            result.model.RawZipFileJudges = result.uploadme1.src.data.split(",")[1]
                        }

                        // Upload Participant Instructions
                        if (result.uploadme2.src != "") {
                            result.model.RawZipFileParticipants = result.uploadme2.src.data.split(",")[1];
                        }

                        projectsHub.invoke("DeleteProject", $scope.currentUser, result.model);
                    }
                });
        }.bind(this);

        this.showAModal = function (project: Project, template:string, controller:string, callback) {
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
    public editProjectModal: any;
    public addProjectModal: any;
    public deleteProjectModal: any;

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
        a.download = name+'.zip';
        document.body.appendChild(a);
        a.click();
    }

}