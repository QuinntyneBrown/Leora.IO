module app.feedback {

    "use strict";

    export class FeedbackManager implements IFeedbackManager {
        constructor(sessionStorageProperty: common.ISessionStorageProperty) {
            this._guid = sessionStorageProperty.instance("guid");
        }

        private _guid: common.ISessionStorageProperty;

        public get guid() {
            return this._guid.get();
        }

        public set guid(value:any) {
            this._guid.set(value);
        }
    }

    angular.module("app.feedback").service("feedbackManager", [
        "sessionStorageProperty",
        FeedbackManager]);
} 