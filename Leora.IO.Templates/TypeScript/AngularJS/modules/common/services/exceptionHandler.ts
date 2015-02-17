module CommonModule {

    angular.module("common").config(["$provide",($provide) => {

        $provide.decorator("$exceptionHandler", ["$delegate","$injector",($delegate, $injector) => {
            return (exception, cause) => {
                $delegate(exception, cause);
                var alerting = $injector.get("alerting");
                alerting.addDanger(exception.message);
            };
        }]);
    }]);
} 