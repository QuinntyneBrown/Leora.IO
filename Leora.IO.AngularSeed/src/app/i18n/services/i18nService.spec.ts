module app.i18n {

    describe("i18nService",() => {

        beforeEach(() => {
            module("app.i18n");
        });

        var i18nService: any;

        var backend: any;

        beforeEach(inject(($httpBackend: ng.IHttpBackendService, apiEndpoint) => {
            backend = $httpBackend;
            backend.expect("POST", apiEndpoint.getBaseUrl() + "/i18n").respond({ translatedPhrases:[]});
        }));

        beforeEach(inject((_i18nService_: any) => {
            i18nService = _i18nService_;
        }));


        it("should be defined",() => {
            expect(i18nService).toBeDefined();
        });

        //it("should get a dto from the /i18n endpoint with an array of translated phrases",() => {
        //    var i18nResults;

        //    i18nService.get({}).then((results) => {
        //        i18nResults = results.data.translatedPhrases;
        //    });

        //    backend.flush();

        //    expect(i18nResults).toEqual([]);
        //});


    });

} 