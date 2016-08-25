/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />


class SystemSrv {

    // Application system variables
    public currentPage: string;

    public SystemSrv() {}

    public setCurrentPage(page: string): void {
        this.currentPage = page;
    }

    public getCurrentPage(): string {
        return this.currentPage;
    }

}