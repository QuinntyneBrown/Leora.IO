module app.ui {

    "use strict";

    export class PaginatedImageListViewer {
        constructor() {

        }

        public static instance = () => {
            return new PaginatedImageListViewer();
        }

        public templateUrl: string = "/src/app/ui/paginatedImageListViewer/paginatedImageListViewer.html";

        public replace: boolean = true;

        public restrict: string = "E";

        public scope: any = {

        }

        public link = () => {

        }
    }

    angular.module("app.ui").directive("paginatedImageListViewer", [PaginatedImageListViewer.instance]);

}