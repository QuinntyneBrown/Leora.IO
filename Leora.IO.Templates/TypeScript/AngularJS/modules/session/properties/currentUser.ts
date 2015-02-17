module SessionModule {

    angular.module("session")
        .service("currentUser", ["$rootScope", "storage",($rootScope, storage) => new CurrentUser($rootScope, storage)] );

    class CurrentUser {
        
        constructor(private $rootScope, private storage) {

            $rootScope.$on("$routeChangeStart",(event, newUrl, oldUrl) => {
                if (newUrl.originalPath == "/signin") {
                    this.data = null;
                    this.set({ data: null });
                }
            });
        }

        public data: any;

        public name:string = "currentUser";

        get = () => {
             if (this.data) {
                 return this.data;
            }

            try {
                this.data = this.storage.getByName({ name: name }).value;
            } catch (error) {

            }

            return this.data;           
        }

        set = (value) => {
            this.data = value.data;
            this.storage.put({ name: name, value: value.data });
        }
    }
}