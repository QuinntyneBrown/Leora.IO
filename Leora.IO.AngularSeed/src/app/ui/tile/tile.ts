module app.ui {

    "use strict";

    export class Tile {
        constructor() {
            
        }

        public static instance = () => {
            return new Tile();
        }

        public restrict: string = "E";

        public replace: boolean = true;

        public scope: any = {};

        public templateUrl: string = "/src/app/ui/tile/tile.html";

        public controller:string = "";

        public controllerAs:string = "";

    }

    angular.module("app.ui").directive("tile", [Tile.instance]);
}