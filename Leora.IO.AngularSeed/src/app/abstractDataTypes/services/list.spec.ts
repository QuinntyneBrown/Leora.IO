module app.abstractDataTypes {

    "use strict";

    describe("List",() => {

        var list: IList<string>;

        beforeEach(() => {
            module("app.abstractDataTypes");
        });

        beforeEach(inject((_list_) => {
            list = _list_;
        }));

        it("should be defined",() => {
            expect(list).toBeDefined();
        });

        it("should have list size of 0",() => {
            expect(list.listSize).toEqual(0);
        });

        it("should be able to append to datastore and increment listSize",() => {
            list.append("Quinn");            
            expect(list.listSize).toEqual(1);
        });

    });

} 