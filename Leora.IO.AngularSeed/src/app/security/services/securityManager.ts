/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.security {

    "use strict";

    import ISessionStorageProperty = common.ISessionStorageProperty;

    import IStorageProperty = common.IStorageProperty;

    export class SecurityManager implements ISecurityManager {

        constructor(sessionStorageProperty: ISessionStorageProperty, storageProperty: IStorageProperty) {
            this._token = sessionStorageProperty.instance("token");
            this._tokenExpiryDate = sessionStorageProperty.instance("tokenExpiryDate");
            this._currentUser = sessionStorageProperty.instance("currentUser");
            this._username = storageProperty.instance("username");
        }

        public _token: ISessionStorageProperty;

        public get token() {
            return this._token.get();
        }

        public set token(value) {
            this._token.set(value);
        }

        public _tokenExpiryDate: ISessionStorageProperty;

        public get tokenExpiryDate() {
            return this._tokenExpiryDate.get();
        }

        public set tokenExpiryDate(value) {
            this._tokenExpiryDate.set(value);
        }

        public _currentUser: ISessionStorageProperty;

        public get currentUser() {
            return this._currentUser.get();
        }

        public set currentUser(value: any) {
            this._currentUser.set(value);
        }

        public _username: IStorageProperty;

        public get username() {
            return this._username.get();
        }

        public set username(value: any) {
            this._username.set(value);
        }

        public tokenExpired = () => {

            if (!this.isAuthenticated)
                return false;

            return Date.now() > this.tokenExpiryDate;
        }

        public get isAuthenticated() {
            return this.token != null;
        }
    }

    angular.module("app.security").service("securityManager", ["sessionStorageProperty", "storageProperty", SecurityManager]);

} 
