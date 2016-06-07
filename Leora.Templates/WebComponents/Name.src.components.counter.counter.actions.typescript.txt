import { IDispatcher, BaseActionCreator, Service } from "../core";

@Service({
    serviceName: "counterActionCreator",
    viewProviders: ["dispatcher", "guid", "invokeAsync"]
})
export class CounterActionCreator {
    constructor(private dispatcher: IDispatcher, private guid, private invokeAsync) { }  
    increment = () => this.dispatcher.dispatch(new Increment());
    decrement = () => this.dispatcher.dispatch(new Decrement());

}

export class Increment { }

export class Decrement { }

