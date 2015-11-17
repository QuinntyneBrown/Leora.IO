module app.common {
    
    export class SessionProvider implements  ISessionProvider {
        constructor() {
            
        }

        public configure = (propertyName: string) => {
            this.properties.push(propertyName);
        }

        public properties: string[] = [];

        public $get = ["$injector", ($injector:ng.auto.IInjectorService) => {

            var session = () => {

            };

            this.properties.forEach((property, index) => {

                var object = $injector.get(property);

                Object.defineProperty(session, property, {
                    get: () => {
                        return object.get();
                    },
                    set: (value) => {
                        object.set(value);
                    }
                });

            });

            return session;
        }];

    }

    angular.module("app.common").provider("session", [SessionProvider]);
}