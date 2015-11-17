declare module app.ui {
    
    export interface IAppHeaderConfigProvider extends ng.IServiceProvider {
        configure(options: any): void;        
    }

    export interface INavigationLink {
        caption: string;
        url: string;
        authenticationRequired?: boolean;        
    }

    export interface IAppHeaderConfig {
        appTitle: string;
        securityManager: string;
        i18nManager: string;
        appManager: string;
    }

    export interface IAppFooterConfigProvider extends ng.IServiceProvider {
        configure(options: any): void;
    }

    export interface IAppFooterConfig {
        appManager:string;
    }

    export interface ITileConfig {
        
    }

    export interface IPopoverScope extends ng.IScope {
        options:any;
    }

    export interface IPopoverServiceProvider extends ng.IServiceProvider {
        
    }

    export interface IPopoverService {
        showPopover(options:any);
    }

    interface IHTMLElement extends HTMLElement {
        style: IStyleCSSProperties;
    }

    interface IStyleCSSProperties extends MSStyleCSSProperties {
        WebkitTransition: string;
        MozTransition: string;
        oTransition: string;
    }

    export interface IModalServiceProvider extends ng.IServiceProvider {

    }

    export interface IModalService {

        showModal(customModalDefaults: any, customModalOptions: any): ng.IPromise<any>;
        isOpen:boolean;

    }

    export interface IPopoverService {
        
    }

    interface IPopUpOptions {

        alignment: string;

        directionPriorityList: string[];

        displayArrow: boolean;

        element: HTMLElement;

        margin: string;

        templateUrl: string;

        triggerScope: IPopUpTriggerScope;

        transitionDurationInMilliseconds: number;

        viewBag: any;

        visibilityDurationInMilliseconds: number;
    }

    interface IPopUpTriggerScope extends ng.IScope {

        templateUrl: string;

        triggerEvent: string;

        directionPriorityList: string[];

        visibilityDurationInMilliseconds: number;

        viewBag: any;

        transitionDurationInMilliseconds: number;

        popUpTrigger: any;

        alignment: string;

        margin: string;

        displayArrow: boolean;
    }


    interface IRating extends ng.IDirective {
        
    }

} 