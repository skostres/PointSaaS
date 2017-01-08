

/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />

/**
 * In charge of defining all client / server methods.
 */
class Communication {

    constructor($rootScope, Hub, $timeout) {
        //declaring the hub connection
        var hub = new Hub('employee', {

            //client side methods
            listeners: {
                'lockEmployee': function (id) {
                   
                    $rootScope.$apply();
                },
                'unlockEmployee': function (id) {
                    
                    $rootScope.$apply();
                }
            },

            //server side methods
            methods: ['lock', 'unlock'],

            //query params sent on initial connection
            queryParams: {
                'token': 'exampletoken'
            },

            //handle connection error
            errorHandler: function (error) {
                console.error(error);
            },

            //specify a non default root
            //rootPath: '/api

            stateChanged: function (state) {
                switch (state.newState) {
                    case $.signalR.connectionState.connecting:
                        //your code here
                        break;
                    case $.signalR.connectionState.connected:
                        //your code here
                        break;
                    case $.signalR.connectionState.reconnecting:
                        //your code here
                        break;
                    case $.signalR.connectionState.disconnected:
                        //your code here
                        break;
                }
            }
        });

        var edit = function (employee) {
            hub.lock(employee.Id); //Calling a server method
        };
        var done = function (employee) {
            hub.unlock(employee.Id); //Calling a server method
        }

        return {
            editEmployee: edit,
            doneWithEmployee: done
        };
    }

}