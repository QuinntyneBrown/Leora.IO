module app.diagnostics {

    "use strict";

    angular.module("app.diagnostics", [
        "ui.router",
        "app.common",
        "app.configuration"
    ])
        .config(["$provide", ($provide: ng.auto.IProvideService) => {

        $provide.decorator("$log", ["$delegate", "$injector", ($delegate: any, $injector: ng.auto.IInjectorService) => {

            var log = (message:string) => {
                var diagnosticsManager = $injector.get("diagnosticsManager");
                var consoleMessages = diagnosticsManager.consoleMessages;
                consoleMessages.push({ message: message });
                diagnosticsManager.consoleMessages = consoleMessages;
                return $delegate.log(message);
            };

            var warn = (message: string) => {
                var diagnosticsManager = $injector.get("diagnosticsManager");
                var consoleMessages = diagnosticsManager.consoleMessages;
                consoleMessages.push({ message: message });
                diagnosticsManager.consoleMessages = consoleMessages;
                return $delegate.info(message);
            };

            var info = (message: string) => {
                var diagnosticsManager = $injector.get("diagnosticsManager");
                var consoleMessages = diagnosticsManager.consoleMessages;
                consoleMessages.push({ message: message });
                diagnosticsManager.consoleMessages = consoleMessages;
                return $delegate.info(message);
            };

            var error = (message: string) => {
                var diagnosticsManager = $injector.get("diagnosticsManager");
                var consoleMessages = diagnosticsManager.consoleMessages;
                consoleMessages.push({ message: message });
                diagnosticsManager.consoleMessages = consoleMessages;
                return $delegate.info(message);
            };

            return {
                log: log,
                warn: warn,
                info: info,
                error: error
            };

            }]);
        }])
        .run(["$location",
        "$rootScope",
        "diagnosticsManager",
        "getWatchersCount", ($location:ng.ILocationService,
            $rootScope: ng.IRootScopeService,
            diagnosticsManager: IDiagnosticsManager,
            getWatchersCount:any) => {

            
        var _consoleLog = console.log;

        console.log = (message:string) => {
            var consoleMessages = diagnosticsManager.consoleMessages;
            consoleMessages.push({ message: message });
            diagnosticsManager.consoleMessages = consoleMessages;
            _consoleLog.apply(console, arguments);
        };

        $rootScope.$on("$locationChangeStart",(event: ng.IAngularEvent, newUrl: string) => {
            
            var urls = diagnosticsManager.urls;
            
            urls.push({
                dateTime: Date.now(),
                url: newUrl,
                path:null,
                changeSuccess: null,
                watchers: null
            });
            diagnosticsManager.urls = urls;
        });


        $rootScope.$on("$viewContentLoaded",() => {
            
            var urls = diagnosticsManager.urls;
            if (urls.length > 1) {
                urls[urls.length - 1].changeSuccess = true;
                urls[urls.length - 1].path = $location.path();
                //urls[urls.length - 1].watchers = getWatchersCount();
            }
            diagnosticsManager.urls = urls;
            
            html2canvas(document.body, {
                onrendered: (canvas: HTMLCanvasElement) => {
                    var image = new Image();
                    image.src = canvas.toDataURL("image/png");
                    var screenCaptures = diagnosticsManager.screenCaptures;
                    screenCaptures.push({
                        dateTime: new Date().toISOString(),
                        base64StringEncodedImage: image.src.split(";")[1].split(",")[1]
                    });
                    diagnosticsManager.screenCaptures = screenCaptures;
                }
            });
        });

    }]);

}