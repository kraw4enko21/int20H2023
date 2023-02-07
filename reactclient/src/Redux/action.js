import {
    API,
    DEC,
    INC
} from "./types";

export function incrementFun() {
    return {
        type: INC
    }
}
export function decrementFun() {
    return {
        type: DEC
    }
}
export function apiFun() {
    return function (dispatch) {
        fetch('https://themealdb.com/images/ingredients/Lime.png')
            .then(response => response.json())
            .then(json => dispatch({type: API, api: json}))
    }
}