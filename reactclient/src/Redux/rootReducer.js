import { combineReducers } from "redux";
import increment from "./increment";

export const rootReducer = combineReducers({
    inc: increment
})