interface IDataService {

    getAll? (): ng.IPromise<any>;

    getById? (id: any): ng.IPromise<any>;

    remove? (id: any): ng.IPromise<any>;

    add? (entity: any): ng.IPromise<any>;

    update? (entity: any): ng.IPromise<any>;

} 