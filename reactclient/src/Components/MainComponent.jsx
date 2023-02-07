import {React,useState,useEffect} from 'react'
import Header from './Header';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { globalUrl } from './url'
import ListDishes from './ListDishes';

import UserProducts from './UserProducts';

export default function MainComponent() {
    const [allDishes, setAllDishes]= useState([])
    useEffect(()=>{
        fetch(globalUrl+'/dishes')
            .then(response => response.json())
            .then(json => setAllDishes(json))
    },[])
  return (
    <BrowserRouter>
        <Header />
        <Routes>
          <Route path='/' element={<ListDishes allDishes={allDishes} setAllDishes={setAllDishes}/>} />
          <Route path='2' element={<UserProducts setAllDishes={setAllDishes}/>} />
        </Routes>


    </BrowserRouter>
  )
}
