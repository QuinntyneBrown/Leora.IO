require("../core/core.module");

import { provide } from "../core";
import { CounterComponent } from "./counter.component";
import { CounterActionCreator } from "./counter.actions";
import *  as reducers from "./counter.reducers";

var app = (<any>angular.module("app.counter", [
    "app.core"    
]));

provide(app,CounterActionCreator);

var qb = 1;

app.component(CounterComponent);

app.config(["reducersProvider", reducersProvider => {	
    
    for (var reducer in reducers) { reducersProvider.configure(reducers[reducer]); }
}]);
