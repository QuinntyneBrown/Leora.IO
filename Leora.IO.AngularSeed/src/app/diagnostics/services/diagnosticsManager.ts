module app.diagnostics {

    "use strict";

    import IStorageProperty = common.IStorageProperty;

    export class DiagnosticsManager implements IDiagnosticsManager {
        constructor(private configurationManager: configuration.IConfigurationManager,
            features: IFeature[],
            storageProperty: IStorageProperty) {
            this._features = features;
            this._requestProfiles = storageProperty.instance("requestProfiles", []);
            this._urls = storageProperty.instance("urls", []);
            this._consoleMessages = storageProperty.instance("consoleMessages", []);
            this._stackTrace = storageProperty.instance("stackTrace", []);
            this._screenCaptures = storageProperty.instance("screenCaptures", []);
        }

        private _features: IFeature[];

        public get features() {
            return this._features;
        }

        private _requestProfiles: IStorageProperty;

        public get requestProfiles() {
            return this._requestProfiles.get();
        }

        public set requestProfiles(value: any) {
            this._requestProfiles.set(value, this.configurationManager.configuration.requestProfilesLimit);
        }

        public updateRequestProfile = (response: any) => {

            var requestProfiles = this.requestProfiles;

            this.requestProfiles.forEach((profile: IRequestProfile) => {
                if (profile.startTime === response.config.timestamp) {
                    profile.elapsed = Date.now() - profile.startTime;
                }
            });

            this.requestProfiles = requestProfiles;
        }

        private _urls: IStorageProperty;

        public get urls() {
            return this._urls.get();
        }

        public set urls(value:any) {
            this._urls.set(value, this.configurationManager.configuration.historyLimit);
        }

        private _consoleMessages: IStorageProperty;

        public get consoleMessages() {
            return this._consoleMessages.get();
        }

        public set consoleMessages(value: any) { 
            this._consoleMessages.set(value, this.configurationManager.configuration.consoleMessagesLimit);
        }

        private _screenCaptures: IStorageProperty;

        public get screenCaptures() {
            return this._screenCaptures.get();
        }

        public set screenCaptures(value:any) {
            this._screenCaptures.set(value, this.configurationManager.configuration.screenCapturesLimit);
        }

        private _stackTrace: IStorageProperty;

        public get stackTrace() {
            return this._stackTrace.get();
        }

        public set stackTrace(value:any[]) {
            this._stackTrace.set(value);
        }
    }

    angular.module("app.diagnostics").service("diagnosticsManager", ["configurationManager","features","storageProperty",DiagnosticsManager]);
} 