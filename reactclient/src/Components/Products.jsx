import { React, useEffect, useState } from 'react'
import ProducItem from './ProducItem'
export default function Products(props) {
  



  function showProducts() {
    return props.info.products.map( (i,n) => <ProducItem setChange={props.setChange} id={props.id} fun={props.setRes} key={n} info={i}/>)
  }
  return (
    <div>
      <p  className="product__title">{props.info.title}</p>
      {
        showProducts()
      }

    </div>
  )
}
