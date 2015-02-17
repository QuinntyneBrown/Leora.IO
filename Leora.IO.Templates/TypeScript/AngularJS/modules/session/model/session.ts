module SessionModule {

    var serviceId = "session";

    angular.module("session").service(serviceId, ["$location", "$http", "$q", "configuration", "configurationService", "currentUser", "token", service]);

    function service(
        $location: ng.ILocationService,
        $http: ng.IHttpService,
        $q: ng.IQService,
        configuration,
        configurationService,
        currentUser,
        token) {

        var self = this;

        self.isLoggedIn = () => {
            if (self.getCurrentUser() != null && self.getCurrentUser() != "") {
                return (self.getCurrentUser().username);
            }
        };

        self.isUserInRole = (roleName) => {
            if (self.isLoggedIn()) {

                var user = self.getCurrentUser();
                for (var i = 0; i < user.roles.length; i++) {
                    if (roleName == user.roles[i].name) {
                        return true;
                    }
                }
            }

            return false;
        };

        self.getCurrentUser = () => {
            return currentUser.get();
        };

        self.signOut = () => {
            $http({ method: "GET", url: "api/identity/signout" }).then(() => {});
            token.set({ data: null });
            currentUser.set({ data: null });
            $location.path("/");
        };

        self.setConfigurationAsync = () => {
            if (configuration.get()) {
                return $q.when(configuration.get());
            }

            return configurationService.get().then((results) => {
                configuration.set({ data: results });
                return configuration.get();
            });
        }

        self.getConfiguration = () => {
            return configuration.get();
        };

        return self;

    }

    export class Session implements ISession {

        public static ServiceId = "session";

        constructor(
            private $location: ng.ILocationService,
            private $http: ng.IHttpService,
            private $q: ng.IQService,
            private configuration: any,
            private configurationService: any,
            private currentUser: any,
            private token: any
        ) {

        }

        isLoggedIn = () => {
            return true;
        };

        isUserInRole = (roleName: string) => {
            return true;
        };

        getCurrentUser = () => {
            return {};
        };

        signOut = () => {
            

        };

        setConfigurationAsync = () => {};

        getConfiguration = () => {
            return {};
        };
    }
}