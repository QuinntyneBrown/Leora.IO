module app.i18n {

    "use strict";

    describe("PhrasesProvider",() => {

        var phrasesProvider:any = null;

        beforeEach(() => {
            phrasesProvider = new app.i18n.PhrasesProvider();
        });

        it("PhrasesProvider", () => {
            expect(phrasesProvider).toBeDefined();
        });

        it("should create a hash number from a string",() => {
            var value: IHashable = <any>"string";
            expect(angular.isNumber(value.hashCode())).toEqual(true);
        });

        it("should create the same hash number from a string",() => {
            var value1: IHashable = <any>"string";
            var value2: IHashable = <any>"string";

            expect(value1.hashCode()).toEqual(value2.hashCode());
        });

        it("should create different hash numbers for different strings",() => {
            var value1: IHashable = <any>"string1";
            var value2: IHashable = <any>"string2";

            expect(value1.hashCode()).not.toEqual(value2.hashCode());
        });

        it("should create a hash number from a complicated phrase",() => {            
            var value: IHashable = <any>"John when to the bank! 8 times? ~";
            expect(angular.isNumber(value.hashCode())).toEqual(true);});
    });

} 