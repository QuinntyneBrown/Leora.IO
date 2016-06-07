export * from "./local-storage-manager-provider";

export interface IAppState {
    lastTriggeredByActionId: any;
    lastTriggeredByAction: any;
}

export type AppState = {
    lastTriggeredByActionId: any;
}

export interface IDispatcher {
    dispatch(action): any;
    subscribe(onNext): void;
}

export interface IStore {
    onNext(action);
    subscribe(onNext);
}

export class InitialStateProvider implements ng.IServiceProvider {
    initialState;
    configure = value => this.initialState = value;
    $get = () => this.initialState;
}

export class ReducersProvider implements ng.IServiceProvider {
    reducers = [];
    configure = value => this.reducers.push(value);
    $get = () => this.reducers;
}

export const functionName = (fun) => {
    var ret = fun.toString();
    ret = ret.substr('function '.length);
    ret = ret.substr(0, ret.indexOf('('));
    return ret;
}

export class Store<T> extends Rx.BehaviorSubject<T> implements IStore {
    constructor(dispatcher: IDispatcher, initialState: T, private localStorageManager, private reducers: any[]) {
        super(initialState || {} as T);
        this.state = initialState || {} as T;        
        dispatcher.subscribe(action => this.onDispatcherNext(action));
    }
    
    onDispatcherNext = (action) => {
        this.state = this.state || {} as T;
        this.state = this.setLastTriggeredByActionId(this.state, action);
        for (var i = 0; i < this.reducers.length; i++) {
            this.state = this.reducers[i](this.state, action);
        }
        this.localStorageManager.put({ name: "initialState", value: this.state });
        this.onNext(this.state);
    }

    setLastTriggeredByActionId = (state, action) => {
        state.lastTriggeredByActionId = action.id;
        state.lastTriggeredByAction = action;
        return state;
    }

    functionToString = fn => {
        return fn.toString();
    }

    state: T;

}

export function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

export class Dispatcher<T> extends Rx.Subject<T> implements IDispatcher {
    constructor() { super() }

    dispatch = action => this.onNext(action);

}

angular.module("store", [])
    .service("store", ["dispatcher", "initialState", "localStorageManager", "reducers", Store])
    .service("dispatcher", [Dispatcher])
    .provider("reducers", ReducersProvider)
    .provider("initialState", InitialStateProvider)
    .value("guid", guid)
    .run(["store", store => { }]);

