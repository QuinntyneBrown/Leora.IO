module app.ui {
    
    export class Tiles {
        constructor() {
            
        }

        public static instance = () => {
            return new Tiles();
        }

        public restrict: string = "E";

        public replace: boolean = true;

        public scope: any = {};

        public templateUrl: string = "/src/app/ui/tiles/tiles.html";

        public link =() => {
            
        }

    }

    angular.module("app.ui").directive("tiles", [Tiles.instance]);
}