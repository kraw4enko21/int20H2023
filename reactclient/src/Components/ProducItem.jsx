import { React, useState, useEffect } from 'react'
import { globalUrl } from './url';
export default function ProducItem(props) {
    const [img, setImg] = useState();
    let [value, setValue] = useState(props.info.quantity)
    const [style , setStyle] = useState('product__save')
    const fetchImage = async () => {
      const res = await fetch(props.info.url);
      const imageBlob = await res.blob();
      const imageObjectURL = URL.createObjectURL(imageBlob);
      setImg(imageObjectURL);
    };
  
    useEffect(() => {
      fetchImage();
    }, []);
    useEffect(()=>{
        if(value!==props.info.quantity)
        {
            setStyle('product__save product__active')
        }
        if(value===props.info.quantity )
        {
            setStyle('product__save')
        }
    },[value])
    console.log(props.info)
    function deleteProduct() {
        fetch(globalUrl + '/deleteUserProduct', {
            method: 'DELETE',
            headers: {
                'accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "userId": props.id,
                "productId": props.info.id
            })

        })
            .then(response => response.json())
            .then(json => {
                props.fun(json.items)
            })

    }
    function changeQuantity()
    {
        fetch(globalUrl + '/changeUserProductQuantity', {
            method: 'PUT',
            headers: {
                'accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "userId": props.id,
                "productId": props.info.id,
                "quantity": value
            })

        })
            .then(response => response.json())
            .then(json => {
                props.fun(json.items)
            })
            setStyle('product__save')
    }
    function newFun() {
        switch (props.info.type) {
            case 'Pieces':
                return 'Шт'
            case 'Grams':
                return 'Гр'
            case 'Millilitres':
                return 'Мл'
            default:
                return ' '
        }
    }
    return (
        <div className="product__item">
            <div className="product__block">
                {/* <ProductsImg info={props.info}/> */}
                <img src={img} alt="logo" />
                <p>{props.info.title}</p>
            </div>
            <div className="product__raiting">
                <button onClick={()=> setValue(--value)} className="product__buttons product__dec">-</button>
                <input value={value} onChange={(e)=>setValue(e.target.value)} type="number" className="product__input" />
                <p>{newFun()}</p>
                <button onClick={()=> setValue(++value)} className="product__buttons product__inc">+</button>
                <button onClick={()=>changeQuantity()} className={style}>SAVE</button>
            </div>
            <button onClick={() => deleteProduct()} className="product__delete"></button>
        </div>
    )
}
