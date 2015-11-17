module app.abstractDataTypes {

    "use strict";

    /**
     * Implement Lazy
     * get data from the server incrementally
     * 
     * UI Optimized
     * only bind the visible elments on the page
     * 
     */
    export class PaginatedList<T> implements IPaginatedList<T> {
        constructor(private $q: ng.IQService,
            private configurationManager: configuration.IConfigurationManager,
            private uiHelper: common.IUIHelper) {

        }

        public instance = <TInstance>(options: any) => {

            var instance = new PaginatedList<TInstance>(this.$q, this.configurationManager, this.uiHelper);

            options = options || {};

            instance.totalItems = options.totalItems || 0;
            instance.pageSize = options.pageSize || 0;
            instance.currentPageItems = options.currentPageItems || [];
            instance.offSet = options.offSet || 0;
            instance.pageYOffSet = 0;
            instance.availHeight = options.availHeight || 0;
            instance.itemHeight = options.itemHeight || 0;
            instance.rows = options.rows || 0;
            instance.top = options.top || 0;
             
            if (options.totalItems && options.totalItems > 0 && options.pageSize) {
                var totalPages = options.totalItems / options.pageSize;

                for (var i = 1; i <= totalPages; i++) {
                    instance.pages.push({
                        startIndex: (i - 1) * options.pageSize,
                        endIndex: (i * options.pageSize) - 1,
                        pageNumber: i
                    });
                }

                if (options.totalItems % options.pageSize) {
                    instance.pages.push({
                        startIndex: (i - 1) * options.pageSize,
                        endIndex: (i * options.pageSize) - 1,
                        pageNumber: i
                    });
                }
            }
            return instance;
        }

        public fetchPageData = (pageNumber: number) => {
            var deferred = this.$q.defer();

            deferred.resolve();

            return deferred.promise;
        }

        private _pages: IPage<T>[] = [];

        public load = (offSet: number, entities: T[]) => {
            entities.forEach((entity:any) => {

            });
        }

        public loadMore = () => {
            
        }

        private _rows: number;

        public get rows() {
            return this._rows;
        }

        public set rows(value: number) {
            this._rows = value;
        }

        private _itemHeight: number;

        public get itemHeight() {
            return this._itemHeight;
        }

        public set itemHeight(value: number) {
            this._itemHeight = value;
        }

        private _pageYOffSet: number;

        public get pageYOffSet() {
            return this._pageYOffSet;
        }

        public set pageYOffSet(value:number) {
            this._pageYOffSet = value;
        }

        public onScroll = (options: any) => {
            this.pageYOffSet = options.pageYOffSet;
        }

        public onResize = (options: any) => {
            this.availHeight = options.availHeight;
            this.pageYOffSet = options.pageYOffSet;
        }

        public _availHeight: number;

        public get availHeight() {
            return this._availHeight;
        }

        public set availHeight(value:number) {
            this._availHeight = value;
        }
        public get pages() {
            return this._pages;
        }

        public set pages(value: IPage<T>[]) {
            this._pages = value;
        }

        private _totalItems: number = 0;

        public get totalItems() {
            return this._totalItems;
        }

        public set totalItems(value: number) {
            this._totalItems = value;
        }

        private _pageSize: number = 0;

        public get pageSize() {
            return this._pageSize;
        }

        public set pageSize(value: number) {
            this._pageSize = value;
        }

        private _dataStore: ICollectionItem<T>[] = [];

        public get dataStore() {
            return this._dataStore;
        }

        public set dataStore(value: any) {
            this._dataStore = value;
        }

        private _currentPageItems: ICollectionItem<T>[] = [];

        public get currentPageItems() {
            return this._currentPageItems;
        }

        public set currentPageItems(value:any) {
            this._currentPageItems = value;
        }

        private _currentPageNumber: number = 0;

        public get currentPageNumber() {

            var currentPageNumber = null;

            this._pages.forEach((page: IPage<T>) => {
                if (this.offSet >= page.startIndex && this.offSet <= page.endIndex) {
                    currentPageNumber = page.pageNumber;
                }
            });

            return currentPageNumber;
        }

        public set currentPageNumber(value:any) {
            this._currentPageNumber = value;
        }

        private _offSet:number;

        public get offSet() {
            return this._offSet;
        }

        public set offSet(value: number) {
            this._offSet = value;
        }

        public hasNext = () => {
            return this.currentPageNumber !== this.pages.length;
        }

        public hasPrevious = () => {
            return this.currentPageNumber !== 1;
        }

        public get nextPageNumber() {
            if (this.hasNext()) {
                return this.currentPageNumber + 1;
            } else {
                return 1;
            }
        }

        public get previousPageNumber() {
            if (this.hasPrevious()) {
                return this.currentPageNumber - 1;
            } else {
                return this.pages.length;
            }
        }

        public get numberOfPages() {
            return this.pages.length;
        }

        public moveTo = (pageNumber: number) => {
            var deferred = this.$q.defer();

            this.fetchPageData(pageNumber).then((results:any) => {
                this.offSet = results.data.offSet;
                this.currentPageItems = results.data.items;
                deferred.resolve();
            });
            return deferred.promise;
        }

        public next = () => {
            return this.moveTo(this.nextPageNumber);
        }

        public prev = () => {
            return this.moveTo(this.previousPageNumber);
        }

        
        public get numberOfViewableItems() {
            if (this.itemHeight) {
                return Math.floor(this.availHeight / this.itemHeight);
            }

            return this.totalItems;
        }

        public _top: number;

        public get top() {
            return this._top;
        }

        public set top(value: number) {
            this._top = value;
        }

        public get indexOfFirstViewableItem() {

            var index = 0;

            if (this.pageYOffSet === 0 || this.itemHeight === 0) {
                return index;
            }

            if (this.top > this.pageYOffSet) {
                return index;
            }

            index = (this.pageYOffSet - this.top) / this.itemHeight;

            return index;
        }

        public get indexOfLastViewableItem() {
            return this.indexOfFirstViewableItem + this.numberOfViewableItems;
        }


        public repository: common.IRepository;
    }

    angular.module("app.abstractDataTypes").service("paginatedList", ["$q", "configurationManager", "uiHelper",PaginatedList]);

} 