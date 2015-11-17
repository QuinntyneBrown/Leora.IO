/// <reference path="../../../scripts/typings/typescriptapp.d.ts" />

declare module app.security {

    export interface ISecurityService {
        tryToLogin(username: string, password: string): ng.IPromise<any>;        
        getCurrentUser(): ng.IPromise<any>;       
    }

    export interface ISecurityManager {
        tokenExpired(): boolean;
        isAuthenticated: boolean;
        currentUser: any;
        tokenExpiryDate: number;
        token: string;
        username: string;
    }

    export interface ISecurityModalService {
        showModal():ng.IPromise<any>;
    }

    export interface ILoginController {
        username: string;
        password: string;
        rememberMe: boolean;
        tryToLogin(): void;
    }

    export interface ILoginRedirectProvider extends ng.IServiceProvider {
        setLoginUrl(url: string): void;
        setDefaultUrl(url:string):void;
    }

    export interface ILoginRedirect {
        setLastPath(path: string):void;
        redirectPostLogin(): void;
        redirectToLogin(): void;
        getLoginUrl():string;
    }
} 
