import {React,useState,useEffect} from 'react'
import Content from './Content'
import Filters from './Filters'
import WeeksDishes from './WeeksDishes'

export default function ListDishes(props) {
    
    return (
        <div className='list__dishes'>
            <WeeksDishes />
            <div className="content__main">
                <div className="container list__block">
                    <Filters setAllDishes={props.setAllDishes}/>
                    <Content allDishes={props.allDishes} setAllDishes={props.setAllDishes} />
                </div>
            </div>
        </div>
    )
}
