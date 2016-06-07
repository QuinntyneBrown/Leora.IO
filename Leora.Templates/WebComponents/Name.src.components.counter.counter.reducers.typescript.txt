import * as actions from "./counter.actions";
import { addOrUpdate, pluckOut } from "../core";

export const counterReducer = (state, action) => {

    state.count = state.count || 0;

    if (action instanceof actions.Increment) 
        state.count = state.count + 1;
    
    if (action instanceof actions.Decrement) 
        state.count = state.count - 1;
    
    return state;
}