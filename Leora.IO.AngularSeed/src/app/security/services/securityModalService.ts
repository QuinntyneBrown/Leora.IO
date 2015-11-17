module app.security {

    "use strict";

    export class SecurityModalService implements ISecurityModalService {

        constructor(private $rootScope: ng.IRootScopeService,
            private $q: ng.IQService,
            private modalService: ui.IModalService,
            private securityManager: ISecurityManager) {

               
        }

        public showModal = () => {

            var deferred = this.$q.defer();

            this.modalService.showModal({
                //controller: "loginModalController as loginModal",
                templateUrl: "/src/app/security/views/partials/login.html"
            }, null).then((results) => {
                deferred.resolve();
            });

            return deferred.promise;
        }
    }

    angular.module("app.security").service("securityModalService", ["$rootScope","$q","modalService","securityManager",SecurityModalService]);

} 