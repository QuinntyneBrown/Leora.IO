 module app.security {

     "use strict";

     export class LoginDirective {
         constructor() {
             
         }

         public static instance() {
             return new LoginDirective();
         }

         public restrict: string = "E";

         public replace: boolean = true;

         public templateUrl: string = "/src/app/security/directives/login/login.html";

         public controller: string = "loginController";

         public controllerAs:string= "login";
     }

     angular.module("app.security").directive("login", [LoginDirective.instance]);
 }