/// <reference path="../../../scripts/typings/typescriptapp.d.ts" />

declare module app.common {


    export interface IDiposable {

    }

    /*
     * IAppManager interface
     *  - a feature has a manager that manages client persisted application variables
     *  - a feature manager can implement IAppManager so that in can control the header and footer
     *  - only one appManager per running running app
     */
    export interface IAppManager {
        navigationLinks: any[];
        userLinks: any[];
        commands: any[];
    }

    export interface IApplicationProvider extends ng.IServiceProvider {
        configure(options: any): void;
    }

    export interface IControlledEntity<T> {
        entity: IEntity;
        isDirty: boolean;
        dataService: IDataService;
        isNew: boolean;
        isDeleted: boolean;
    }

    export interface IRepository {
        getPage? (offSet: number, setSize: number): ng.IPromise<any>;
    }

    export interface IEntity extends IEquals, IRepository {
        id?: number;
        instance? (data: any): ng.IPromise<any>;
        getById? (id: any): ng.IPromise<any>;
        getAll? (): ng.IPromise<any>;
        getPage? (offSet: number, setSize: number): ng.IPromise<any>;
        save? (): ng.IPromise<any>;
        remove? (): ng.IPromise<any>;
        isValid? (): ng.IPromise<any>;
        getValidationErrors? (): ng.IPromise<any>;
    }

    export interface IApiEndpointConfig {
        baseUrls: IEndpointDefinition[];
        getBaseUrl(name?: string): string;
    }

    export interface IEndpointDefinition {
        name?: string;
        url: string;
    }

    export interface IApiEndpointProvider extends ng.IServiceProvider {
        configure(baseUrl: string, name?: string): void;
    }

    export interface IBind {
        (element: HTMLElement, object: any): void;
    }

    export interface IFire {

        (target: HTMLElement, type: string, properties: any): void;
    }

    export interface IHistoryService {

    }

    export interface IModelState {

    }

    export interface INotificationService {

    }

    export interface IRequestCounter {
        getRequestCount(): number;
    }

    export interface IFeatureComponentsMapping {
        feature: string;
        components: string[];
    }

    export interface IRouteConfig {
        path: string;
        component?: string;
        redirectTo?: string;
    }

    export interface IFeatureComponentsMappingsProvider extends ng.IServiceProvider {
        mapFeatureComponents? (featureComponentsMapping: IFeatureComponentsMapping): void;
        setTemplateMapping($componentLoaderProvider: any): void;
    }

    export interface IRoutesProvider extends ng.IServiceProvider {
        routes: IRouteConfig[];
        mapRoutes(routes: IRouteConfig[]);
    }

    export interface IFormEncode {
        (data: any): string;
    }

    export interface IDataService {
        fromCacheOrService(action: IHttpAction, cacheKey: string): ng.IPromise<any>;
        fromService(action: IHttpAction, cacheKey?: string): ng.IPromise<any>;
        fromCache(action: IHttpAction): any;
        invalidateCache(cacheKey: string): void;
    }

    export interface IViewDataPrimer {
        resolveViewData(): ng.IPromise<any>;
    }

    export interface IHttpAction {
        method: string;
        url: string;
        data?: any;
        params?: any;
        configuration?: any;
    }

    export interface INameValuePair {
        category?: string;
        name: string;
        value?: any;
    }

    export interface ISessionStorageProperty {
        instance? (name: string, initialValue?: any): ISessionStorageProperty;
        get(): any;
        set(value: any, limit?: number): void;
        push? (value: any): void;
    }

    export interface IStorageProperty {
        instance? (name: string, initialValue?: any): IStorageProperty;
        data?: any;
        get(): any;
        set(value: any, limit?: number): void;
        push? (value: any): void;
    }

    export interface IStorageArrayProperty {
        instance? (name: string): IStorageProperty;
        data?: any;
        get(): any;
        set(value: any): void;
        push(value: any): void;
    }

    export interface ISessionStorageArrayProperty {
        instance? (name: string): IStorageProperty;
        data?: any;
        get(): any;
        set(value: any): void;
        push(value: any): void;
    }

    export interface IStorage {
        put(params: INameValuePair): void;
        get(): any;
        getByName(params: INameValuePair): any;
    }

    export interface IMetadataService {
        getMetadata(): ng.IPromise<any>;
        getMetadataForEntity(name: string): ng.IPromise<any>;
        getLocalMetadataForEntity(name: string): any;
    }

    export interface ILookUpListService {
        getByName(name: string, nomenclatureId: number, cultureId: number): ng.IPromise<any>;
    }

    export interface IEquals {
        equals? (entityA: any, entityB: any): boolean
    }

    export interface IStatePromise {
        priority: number;
        state?: string;
        exludedStates?: string[];
        promise: any;
        key?: string;
        cancelCallback?: boolean;
    }

    export interface IStatePromisesPrioritizedGroup {
        promises: IStatePromise[];
        priority: number;
        isLast: boolean;
    }

    export interface IStateResolverProvider {
        configure(promise: IStatePromise): void;
    }

    export interface IStateResolver {
        resolve(stateName: string, $stateParams?: ng.ui.IStateParamsService): ng.IPromise<any>;
        registerDeactivation(deactivatableObject: IDeactivatable): void;
        getStateParams(): ng.ui.IStateParamsService;
    }

    export interface IStateParamsHelper {
        getParams(paramIds: string[], stateUrl?: string): any;
    }

    export interface IPrioritizedPromisesService {
        instance($q: ng.IQService): IPrioritizedPromisesService;
        add(func: IPrioritizableAsyncFunc): IPrioritizedPromisesService;
        resolve(): ng.IPromise<any>;
        processAll(stack: IPrioritizableAsyncFunc[], index: number, callback: any);
        next(stack: IPrioritizableAsyncFunc[], index: number, callback: any);
    }

    export interface IPrioritizableAsyncFunc {
        func(options: any): ng.IPromise<any>;
        priority: number;
        options: any;
    }

    export interface ISessionProvider {
        configure(propertyName: string): void;
    }

    export interface IUIHelper {
        currentFormFactor: formFactor;
        clientRectEquals(clientRectA: ClientRect, clientRectB: ClientRect): boolean;
        getBoundingRectForDetachedElement(detachedElement: HTMLElement): ClientRect;
        getSurroundingWindowSpace(element: HTMLElement, _window: Window): IRect;
        positionDetachedElement(
            triggerElement: HTMLElement,
            element: HTMLElement,
            directionPriorityList: string[],
            elementRect: ClientRect,
            alignment: string,
            elementSurroundingWindowSpaceRect: IRect): IPositionDetachedElementResponseDto;
    }

    export interface IRect {
        top: number;
        left: number;
        right: number;
        bottom: number;
    }

    export interface IPositionDetachedElementResponseDto {
        position: string;
        elementRect: ClientRect;
    }

    export interface ILocalStorageService {
        set(key: string, value: any): any;
        get(key: string): any;
        remove(key: string): boolean;
        clearAll(): void;

        bind($scope: ng.IScope, key: string, opts?: {
            defaultValue?: any;
            storeName?: string;
        }): any;
        unbind($scope: ng.IScope, key: string, storeName?: string): void;
    }

    export interface IPagedEntities {
        instance(items: any[], setSize: number, offSet: number, total: number): IPagedEntities;
        // page: T[];
        // currentPageNumber: number;
        // numberOfPages: number;

    }

    export interface IDeactivatable {
        canDeactivate? (): boolean;
        deactivate(): ng.IPromise<any>;
    }
}