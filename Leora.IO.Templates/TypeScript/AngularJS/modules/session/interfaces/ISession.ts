module SessionModule {
    
    export interface  ISession {

        isLoggedIn(): Boolean;

        isUserInRole(roleName: string): Boolean;

        getCurrentUser(): any;

        signOut(): void;

        setConfigurationAsync(): void;

        getConfiguration(): any;

    }
} 