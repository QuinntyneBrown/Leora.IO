/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.feedback {

    angular.module("app.feedback").config(["$provide", ($provide) => {

        $provide.decorator("$exceptionHandler", ["$delegate", "$injector", ($delegate, $injector) => {

            return (exception, cause) => {

                if (exception.message === "Unable to get property 'focus' of undefined or null reference")
                    return;

                $delegate(exception, cause);



                var timestamp = Date.now();
                var diagnosticsManager: diagnostics.IDiagnosticsManager = $injector.get("diagnosticsManager");
                var feedbackService: IFeedbackService = $injector.get("feedbackService");
                var feedbackManager: IFeedbackManager = $injector.get("feedbackManager");
                var printStackTrace: diagnostics.IPrintStackTrace = $injector.get("printStackTrace");
                var $location: ng.ILocationService = $injector.get("$location");
                var errorRedirect: diagnostics.IErrorRedirect = $injector.get("errorRedirect");

                diagnosticsManager.consoleMessages.push({ message: cause });

                feedbackService.save({
                    consoleMessages: diagnosticsManager.consoleMessages,
                    stackTrace: printStackTrace({ e: exception }),
                    screenCaptures: diagnosticsManager.screenCaptures,
                    requestProfiles: diagnosticsManager.requestProfiles,
                    features: diagnosticsManager.features
                }).then((results: any) => {
                    feedbackManager.guid = results.data.guid;
                    $injector.get("feedbackModalService").showModal().then((results) => {
                        errorRedirect.redirectPostError({ timestamp: timestamp });
                    });
                });
            };
        }]);
    }]);
} 