import { IAppState, IStore } from "./store";

angular.module("invokeAsync", []).value("invokeAsync", options => {
    let store: IStore = angular.element(document.body).injector().get("store");
    let $q: angular.IQService = angular.element(document.body).injector().get("$q");    
    if (angular.isFunction(options)) { options = { action: options } };
    let deferred = $q.defer();
    let actionId = options.params ? options.action(options.params) : options.action();
    let subscription = store.subscribe((state: IAppState) => {
        if (state.lastTriggeredByActionId == actionId) {
            if(subscription)
                subscription.dispose();
            deferred.resolve();
        }
    });
    return deferred.promise;
});