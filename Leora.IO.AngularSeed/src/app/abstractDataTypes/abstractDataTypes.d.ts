declare module app.abstractDataTypes {
    
    export interface IObservable<T> {
        subscribe(observer: IObserver<T>): common.IDiposable
    }

    export interface IObserver<T> {
        onNext(value: T): void;
        onError(error: Error): void;
        onCompleted(): void;
    }

    export interface IIterable<T> {
        //[](): IIterator<T>;
    }

    export interface IIterator<T> {
        next(): IIteratorResult<T>
    }

    export interface IIteratorResult<T> {
        done: boolean;
        value:T;
    }

    
    export interface IEntity {
        instance(options?: any):ng.IPromise<any>;
        getPage?(options?:any):ng.IPromise<any>;
    }

    export interface ICollectionItem<T> {
        index: number;
        entity: T;
    }

    export interface ISequentialPromise {
        index?:number;
        func(options:any): ng.IPromise<any>;
        options:any;
    }


    export interface ISequentialPromiseList {
        instance():ISequentialPromiseList;
        push(promise: ISequentialPromise):void;
        resolve():ng.IPromise<any>;    
    }

    export interface IList<T> {

        instance<TInstance>(): IList<TInstance>;

        listSize:number;

        pos:number;

        length:number;

        clear():void;  

        toString():any;  

        getElement(pos: number):T;  

        insert(elemnt: T):void;
    
        append(element: T): void;  

        remove(element: T): void;  

        front(): void;  

        end(): void;  

        prev(): void;  

        next(): void;  

        currPos(): void;  

        moveTo():void;

    }

    export interface IPage<T> {
        pageNumber: number;
        startIndex: number;
        endIndex: number;
        isLoaded?:boolean;
    } 

    export interface IPaginatedList<T> {
        instance<TInstance>(options: any): IPaginatedList<TInstance>;
        updateListener?(event:MSEventObj): void;
        repository: common.IRepository;
        currentPageItems: ICollectionItem<T>[];
        dataStore: ICollectionItem<T>[];
        currentPageNumber: number;
        pages: IPage<T>[];
        loadMore():void;
        pageSize: number;
        totalItems: number;
        offSet: number;
        hasNext(): boolean;
        hasPrevious(): boolean;
        nextPageNumber: number;
        previousPageNumber: number;
        numberOfPages: number;
        moveTo(pageNumber: number): ng.IPromise<any>;
        next(): ng.IPromise<any>;
        prev(): ng.IPromise<any>;
        pageYOffSet: number;
        availHeight:number;
        onScroll(options: any): void;
        onResize(options: any): void;
        itemHeight: number;
        rows: number;
        numberOfViewableItems: number;
        indexOfFirstViewableItem: number;
        indexOfLastViewableItem: number;
        top: number;
    }
} 