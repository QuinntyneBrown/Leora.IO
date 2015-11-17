 module app.common {

     describe("StateResolver", () => {

         var stateResolverProvider;
         var scope;
         var routeData;
         var $injector;

         beforeEach(() => {
             module("app.common");
             stateResolverProvider = new app.common.StateResolverProvider();
             routeData = null;
         });

         beforeEach(inject(($rootScope, _$injector_) => {
             scope = $rootScope.$new();
             $injector = _$injector_;
         }));


         it("should be defined",() => {
             expect(stateResolverProvider).toBeDefined();
         });

         it("should be have 0 promises",() => {
             expect(stateResolverProvider.statePromises).toEqual([]);
         });

         it("should be have 1 promise if configured with 1 promise",() => {

             stateResolverProvider.configure({
                 priority: 1,
                 state: "default",
                 promise: [
                     "$q", ($q: ng.IQService) => { return $q.when(true); }]
         });

             expect(stateResolverProvider.statePromises.length).toEqual(1);
         });

         it("should be have 1 promise if configured with 1 promise",() => {

             stateResolverProvider.configure({
                 priority: 1,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                        routeData = 1;
                        return $q.when(routeData);
                 }]
             });

             expect(stateResolverProvider.statePromises.length).toEqual(1);

             var stateResolver = $injector.invoke(stateResolverProvider.$get);

             expect(stateResolver).toBeDefined();

             expect(routeData).toBeFalsy();

             stateResolver.resolve("default");

             scope.$digest();

             expect(routeData).toEqual(1);

         });

         it("it should group state promises by priority",() => {

             var statePromises = [];

             statePromises.push({
                 priority: 1,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                         routeData = 1;
                         return $q.when(routeData);
                     }]
             });

             statePromises.push({
                 priority: 2,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                         routeData = routeData + 1;
                         return $q.when(routeData);
                     }]
             });

             statePromises.push({
                 priority: 1,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                         routeData = 1;
                         return $q.when(routeData);
                     }]
             });

             statePromises.push({
                 priority: 2,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                         routeData = routeData + 1;
                         return $q.when(routeData);
                     }]
             });

             var result = stateResolverProvider.groupStatePromisesByPriority(statePromises);

             expect(result.length).toEqual(2);
             expect(result[1].isLast).toEqual(true);
         });

         it("should be have 2 promises if configured with 2 promises",() => {

             stateResolverProvider.configure({
                 priority: 1,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                         routeData = 1;
                         return $q.when(routeData);
                     }]
             });

             stateResolverProvider.configure({
                 priority: 2,
                 state: "default",
                 key: "testRouteData",
                 promise: [
                     "$q", ($q: ng.IQService) => {
                         routeData = routeData + 1;
                         return $q.when(routeData);
                     }]
             });

             expect(stateResolverProvider.statePromises.length).toEqual(2);

             var stateResolver = $injector.invoke(stateResolverProvider.$get);
             var routeDataResult:any;
             expect(stateResolver).toBeDefined();

             expect(routeData).toBeFalsy();

             stateResolver.resolve("default").then((results) => {
                 routeDataResult = results;
             });

             scope.$digest();

             expect(routeData).toEqual(2);

             expect(routeDataResult.testRouteData).toEqual(2);

         });
     });

 }