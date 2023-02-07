import React, { useEffect,useState } from 'react'
import search from '../img/search.png'
import Dishe from './Dishe'
import ModalDishes from './ModalDishes'

export default function Content(props) {
    
   
    // const [infoModal , setInfoModal]= useState()
    
    function showDishes()
    {
        const local = [];

        for( let i in props.allDishes.items)
        {
            local[i]=  <Dishe key={i} info={props.allDishes.items[i]}  />
        }
        return local
        
    }
    return (
        <div className='content'>
            <p className="title">Страви</p>
            <div className="search__block">
                <input className='search' type="text" placeholder='почніть вводити страву' />
                <img className='search__img' src={search} alt="search" />
            </div>
            
            {
                showDishes()
            }
            
        </div>
    )
}
