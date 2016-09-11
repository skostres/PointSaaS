class BaseCtrl {

    public $scope;
    public jQuery;

    public constructor($scope, jQuery) {
        this.$scope = $scope;
        this.jQuery = jQuery;
    }
}