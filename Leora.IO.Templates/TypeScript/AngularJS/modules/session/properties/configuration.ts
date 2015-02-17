module SessionModule {

    "use strict";

    var serviceId = "configuration";

    angular.module("session").service(serviceId, ["$rootScope", "storage", service]);

    function service($rootScope, storage) {
        var self = this;
        var data = null;
        var name = "configuration";

        self.get = function get() {
            if (data) {
                return data;
            }

            try {
                data = storage.getByName({ name: name }).value;
            } catch (error) {
            }
            return data;
        };

        self.set = function set(params) {
            data = params.data;
            storage.put({ name: name, value: params.data });
        };

        $rootScope.$on("$routeChangeStart", function routeChange(event, newUrl, oldUrl) {
            if (newUrl.originalPath == "/signin") {
                data = null;
                self.set({ data: null });
            }
        });
        return self;
    }

}
