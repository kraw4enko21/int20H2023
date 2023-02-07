import { API, DEC, INC } from "./types";

export default function increment(state = 0, action)
{
    if( action.type === DEC)
    {
        return --state
    }
    else if( action.type === INC)
    {
        return ++state
    }
    else if( action.type === API)
    {
        console.log(action.api)
        return state
    }
    else
    {
        return state
    }
    
} 