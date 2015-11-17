module app.ui {

    "use strict";

    export class Rating implements IRating {
        constructor() {
            
        }

        public static instance = () => {
            return new Rating();
        }

        public templateUrl: string = "/src/app/ui/rating/rating.html";

        public replace: boolean = true;

        public restrict: string = "E";

        public scope:any = {
            maxRating: "=",
            ngModel:"="
        }

        public link = () => {
            
        }
    }

    angular.module("app.ui").directive("rating", [Rating.instance]);
} 