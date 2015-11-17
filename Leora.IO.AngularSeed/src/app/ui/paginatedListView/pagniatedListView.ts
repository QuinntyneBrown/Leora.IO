module app.ui {

    export class PaginatedListView {
        constructor() {

        }

        public static instance() {
            return new PaginatedListView();
        }

        public templateUrl: string = "/src/app/ui/paginatedListView/paginatedListView/recentPoList.html";

        public replace: boolean = true;

        public restrict: string = "E";

        public scope: any = {
            ngModel: "="
        };

        public link = () => {

        }
    }

    angular.module("app.ui").directive("paginatedListView", [PaginatedListView.instance]);
}