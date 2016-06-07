describe("counter", () => {

    var counterActionCreator;

    beforeEach(() => {
        angular.mock.module("app.counter");
    });

    beforeEach(inject((_counterActionCreator_) => {
        counterActionCreator = _counterActionCreator_;
    }));

    it("should be defined", () => {
        expect(counterActionCreator).toBeDefined();
    });
})