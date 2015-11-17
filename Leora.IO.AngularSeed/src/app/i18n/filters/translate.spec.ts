module app.i18n {

    describe("Translate Filter",() => {

        beforeEach(() => {
            module("app.i18n");
        });

        (<any>String.prototype).hashCode = function () {
            var hash = 0, i, chr, len;
            if (this.length == 0) return hash;
            for (i = 0, len = this.length; i < len; i++) {
                chr = this.charCodeAt(i);
                hash = ((hash << 5) - hash) + chr;
                hash |= 0;
            }
            return hash;
        };

        var translate: any;

        var mockI18nManager: II18nManager = {
            cultureCode: "en-US",
            translatedPhrases: {
                cultureCode: "en-US",
                phrases: [
                    {
                        hashCode: (<any>"something").hashCode(),
                        text:"something",
                        translation:"something else"
                    }
                ]
            }            
        };

        beforeEach(() => {
            module(($provide) => {
                $provide.value('i18nManager', mockI18nManager);
            });
        });

        beforeEach(inject(($filter) => {
            translate = $filter("translate");
        }));

        it("should be defined",() => {
            expect(translate).toBeDefined();
        });

        it("should translate a phrase that has been provided via the i18n service",() => {
            var result = translate("something");

            expect(result).toEqual("something else");
        });

        it("should use the translated phrases provided",() => {
            var result = translate("something", {
                phrases: []
            });

            expect(result).toEqual("something");
        });
    });

} 