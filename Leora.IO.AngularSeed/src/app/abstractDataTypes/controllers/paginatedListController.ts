module app.abstractDataTypes {

    "use strict";

    export class PaginatedListController<T> {
        constructor(paginatedList:IPaginatedList<T>, private routeData:any) {
            this._paginatedList = paginatedList.instance<T>({
                items: routeData.items,
                totalItems: routeData.totalItems,
                repository: routeData.repository
            });
        }

        public _paginatedList: IPaginatedList<T>;

        public get currentPageItems() {
            return this._paginatedList.currentPageItems;
        }

        public get totalItems() {
            return this._paginatedList.totalItems;
        }

        public loadMore = () => {
            return this._paginatedList.loadMore();
        }

    }

    angular.module("app.abstractDataTypes").controller("paginatedListController", ["routeData",PaginatedListController]);
} 