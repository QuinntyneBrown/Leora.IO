describe("counter", () => {

    var counterComponent;

    class MockActionCreator { }

    beforeEach(() => {
        angular.mock.module("app.counter");
    });

    beforeEach(inject(($controller) => {
        counterComponent = $controller("counterComponent", { counterActionCreator: new MockActionCreator() });
    }));

    it("should be defined", () => {
        expect(counterComponent).toBeDefined();
    });
})