module app.abstractDataTypes {

    "use strict";

    describe("PaginatedList", () => {

        var paginatedList: IPaginatedList<any>;

        var $window;

        var scope;

        beforeEach(() => {
            module("app.abstractDataTypes");
        });

        beforeEach(inject((_paginatedList_: any, _$rootScope_:any, _$window_: any) => {
            paginatedList = _paginatedList_.instance();
            $window = _$window_;
            scope = _$rootScope_.$new();
        }));

        it("should be defined",() => {
            expect(paginatedList).toBeDefined();
        });

        it("should have default of 0 total items", () => {
            expect(paginatedList.totalItems).toEqual(0);
        });

        it("expect to set total items through the instance constructor",() => {
            var instance = paginatedList.instance({ totalItems: 10 });
            expect(instance.totalItems).toEqual(10);
        });

        it("should have default page size of 0",() => {
            expect(paginatedList.pageSize).toEqual(0);
        });

        it("expect to set page size through the instance constructor along with total items",() => {
            var instance = paginatedList.instance({ pageSize: 10 });
            expect(instance.pageSize).toEqual(10);
        });

        it("expect currentPageItems to be an empty array",() => {
            expect(paginatedList.currentPageItems).toEqual([]);
        });

        it("expect to currentPageItems to be set through constructor",() => {
            var instance = paginatedList.instance<string>({ currentPageItems: [{ index: 0, entity: "item" }] });
            expect(instance.currentPageItems).toEqual([{ index: 0, entity: "item" }]);
        });

        it("expect currentPageItems to be an empty array",() => {
            expect(paginatedList.currentPageItems).toEqual([]);
        });

        it("should have an initial currentPageNumber of 0",() => {
            expect(paginatedList.currentPageNumber).toEqual(null);
        });
        

        it("should have 2 pages, if you pass offSet of 0 and pageSize of 10 and total items of 20",() => {
            var instance = paginatedList.instance<String>({offSet:0, pageSize:10, totalItems:20});
            expect(instance.pages.length).toEqual(2);

        });

        it("should have 3 pages, if you pass offSet of 0 and pageSize of 10 and total items of 21",() => {
            var instance = paginatedList.instance<String>({ offSet: 0, pageSize: 10, totalItems: 21 });
            expect(instance.pages.length).toEqual(3);

        });

        it("should have a currentPageNumber of 1, if you pass offSet of 0 and pageSize of 10 and total items of 10", () => {
            var instance = paginatedList.instance<String>({ offSet: 0, pageSize: 10, totalItems: 21 });
            expect(instance.currentPageNumber).toEqual(1);

        });

        it("should have a currentPageNumber of 3, if you pass offSet of 7 and pageSize of 3 and total items of 15",() => {
            var instance = paginatedList.instance<String>({ offSet: 7, pageSize: 3, totalItems: 15 });
            expect(instance.currentPageNumber).toEqual(3);

        });

        it("should have a hasNext value of true if current page is 3 when you initiallize with a offSet of 7 and pageSize of 3 and total items of 15",() => {
            var instance = paginatedList.instance<String>({ offSet: 7, pageSize: 3, totalItems: 15 });
            expect(instance.hasNext()).toEqual(true);

        });

        it("should have a havePrevious value of true if current page is 3 when you initiallize with a offSet of 7 and pageSize of 3 and total items of 15",() => {
            var instance = paginatedList.instance<String>({ offSet: 7, pageSize: 3, totalItems: 15 });
            expect(instance.hasPrevious()).toEqual(true);

        });

        it("should have a pageYOffSet of 0",() => {
            var instance = paginatedList.instance<String>({ offSet: 7, pageSize: 3, totalItems: 15 });
            expect(instance.pageYOffSet).toEqual(0);
        });
        
        it("should have a pageYOffSet of 0 same as window",() => {
            var instance = paginatedList.instance<String>({ offSet: 7, pageSize: 3, totalItems: 15 });
            expect(instance.pageYOffSet).toEqual(0);
            instance.onScroll({ pageYOffSet: 100 });
            expect(instance.pageYOffSet).toEqual(100);
        });

        it("should have a default available height of 0",() => {
            var instance = paginatedList.instance<String>({ offSet: 7, pageSize: 3, totalItems: 15 });
            expect(instance.availHeight).toEqual(0);
        });

        it("should have a default available height of 110",() => {
            var instance = paginatedList.instance<String>({ availHeight: 110 });
            expect(instance.availHeight).toEqual(110);
        });

        it("should have a new available height after resize",() => {
            var instance = paginatedList.instance<String>({ availHeight: 110 });
            instance.onResize({ availHeight: 0 });
            expect(instance.availHeight).toEqual(0);
        });

        it("should have an itemHeight default 0",() => {
            var instance = paginatedList.instance<String>({});            
            expect(instance.itemHeight).toEqual(0);
        });

        it("should have an item height of 100",() => {
            var instance = paginatedList.instance<String>({ itemHeight: 100 });
            expect(instance.itemHeight).toEqual(100);
        });

        it("should have a first viewable item index of 0", () => {
            var instance = paginatedList.instance<String>({ itemHeight: 100 });
            expect(instance.indexOfFirstViewableItem).toEqual(0);
        });

        it("should have a first viewable item index of 1",() => {
            var instance = paginatedList.instance<String>({ itemHeight: 10 });
            instance.onScroll({ pageYOffSet: 10 });
            expect(instance.indexOfFirstViewableItem).toEqual(1);
        });

        it("should have a first viewable item index of 1",() => {
            var instance = paginatedList.instance<String>({ itemHeight: 10, top: 10 });
            instance.onScroll({ pageYOffSet: 10 });
            expect(instance.indexOfFirstViewableItem).toEqual(0);
        });
    });

} 