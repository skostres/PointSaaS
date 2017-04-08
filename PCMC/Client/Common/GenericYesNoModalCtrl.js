var GenericYesNoModalCtrl = (function () {
    function GenericYesNoModalCtrl($scope, $element, model, close) {
        $scope.param1 = 0;
        $scope.model = model;
        $scope.uploadme1 = {};
        $scope.uploadme1.src = "";
        $scope.uploadme2 = {};
        $scope.uploadme2.src = "";
        //  This close function doesn't need to use jQuery or bootstrap, because
        //  the button has the 'data-dismiss' attribute.
        $scope.close = function () {
            close({
                model: $scope.model,
                uploadme1: $scope.uploadme1,
                uploadme2: $scope.uploadme2,
                param1: $scope.param1,
                cancel: false
            }, 500); // close, but give 500ms for bootstrap to animate
        }.bind(this);
        //  This cancel function must use the bootstrap, 'modal' function because
        //  the doesn't have the 'data-dismiss' attribute.
        $scope.cancel = function () {
            //  Manually hide the modal.
            $element.modal('hide');
            //  Now call close, returning control to the caller.
            close({
                cancel: true
            }, 500); // close, but give 500ms for bootstrap to animate
        }.bind(this);
        $scope.downloadPreSubmission = function (name, file) {
            this.downloadFile(name, file.data.split(",")[1]);
        }.bind(this);
        // Data is Base64 string
        $scope.downloadFile = function (name, data) {
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
        }.bind(this);
    }
    GenericYesNoModalCtrl.prototype.base64ToHex = function (str) {
        for (var i = 0, bin = atob(str.replace(/[ \r\n]+$/, "")), hex = []; i < bin.length; ++i) {
            var tmp = bin.charCodeAt(i).toString(16);
            if (tmp.length === 1)
                tmp = "0" + tmp;
            hex[hex.length] = tmp;
        }
        return hex.join(" ");
    };
    return GenericYesNoModalCtrl;
}());
