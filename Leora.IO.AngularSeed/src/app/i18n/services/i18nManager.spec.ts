module app.i18n {

    describe("i18nManager", () => {

        var amMoment;
        var i18nManager;
        var $window;
        var mockLocalStorageService: any;
        var mockWindow: any;
        var _localStorage: any = {};
         

        beforeEach(() => {
            module("app.i18n");
            module(($provide) => {
                $provide.value('localStorageService', mockLocalStorageService);
                $provide.value('$window', mockWindow);
            });

            _localStorage = {};

            mockWindow = {
                navigator: {
                    language: "",
                    userLanguage: ""
                }
            };

            mockLocalStorageService = {
                get: (name) => {
                    return _localStorage[name];
                },
                set: (name, value) => {
                    _localStorage[name] = value;
                }
            };
        });

        beforeEach(inject((_i18nManager_, _$window_) => {
            i18nManager = _i18nManager_;
            $window = _$window_;
        }));

        it("should be defined",() => {
            expect(i18nManager).toBeDefined();
        });

        it("should be en-US if window language is en-CA",() => {
            mockWindow.navigator.language = "en-CA";
            mockWindow.navigator.userLanguage = "en-CA";    
            expect($window.navigator.language).toEqual("en-CA");  
            expect(mockLocalStorageService.get("cultureCode")).toBeUndefined();    
            expect(i18nManager.cultureCode).toEqual("en-US");
        });

        it("should have cultureCode equal to localStorage value if available",() => {
            mockLocalStorageService.get = (name:string,value:any) => {
                return "fr-CA";
            };
            expect(i18nManager.cultureCode).toEqual("fr-CA");
        });


        it("should have be able to change cultureCodes",() => {            
            expect(i18nManager.cultureCode).toEqual("en-US");
            i18nManager.cultureCode = "fr-CA";
            expect(i18nManager.cultureCode).toEqual("fr-CA");
            expect(mockLocalStorageService.get("cultureCode")).toEqual("fr-CA");             
        });
    });

}