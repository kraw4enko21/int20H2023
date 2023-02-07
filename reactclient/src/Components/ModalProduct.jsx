import React, { useEffect, useState } from 'react'
import Form from 'react-bootstrap/Form';
import { globalUrl } from './url';
export default function ModalProduct(props) {
    const [state, setState] = useState()
    const [value, setValue] = useState()
    const [val, setVal] = useState()
    const [par, setPar] = useState()
    const [all, setAll] = useState()
    const [style, setStyle] = useState('error ')
    const [roz, setRoz] = useState('')
    console.log(props.id)
    useEffect(() => {
        fetch(globalUrl + '/products')
            .then(response => response.json())
            .then(json => {
                setAll(json)
                const arr = []
                for (let i in json.items) {
                    arr.push(json.items[i].title)
                }
                setState(arr.map((i, n) => <option key={n} value={n} >{i} </option>))
            })
    }, [])
    function newFun(i) {
        setValue(i)
        switch (all.items[i].measurmentType) {
            case 'Pieces':
                setRoz('Шт')
                break;
            case 'Grams':
                setRoz('Гр')
                break;

            case 'Millilitres':
                setRoz('Мл')
                break;
            default:
                setRoz('')
        }
    }
    function save() {
        if (!value) {
            setPar('Оберіть продукт')
            setStyle('error active')
            setTimeout(() => {
                setStyle('error')
            }, 3000)
        }
        else if (val === undefined) {
            setPar('Введіть кількість')
            setStyle('error active')
            setTimeout(() => {
                setStyle('error')
            }, 3000)
        }
        else {
            fetch(globalUrl + '/addUserProduct', {
                method: 'POST',
                headers: {
                    'accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    "userId": props.id,
                    "productId": all.items[value].id,
                    "quantity": val
                })

            })
                .then(response => response.json())
                .then(json => {
                    props.setRes(json)
                })
            setVal(0)
            props.setActive(false)
            

        }
    }
    return (
        <div className={props.active ? 'newModal active  ' : ' newModal'} onClick={() => props.setActive(false)}>
            <div className={props.active ? 'modalproduct__content active  ' : 'modalproduct__content '} onClick={e => e.stopPropagation()}>
                <p className="title">Додавання інгредієнтів</p>
                <div className="modalBold">Продукт</div>
                <Form.Select onChange={(e) => newFun(e.target.value)} aria-label="Default select example">
                    <option value={false} >Оберіть продукт зі списку</option>
                    {
                        state
                    }
                </Form.Select>
                <div className="modal__miniblock">
                    <p className="modalBold">Кількість</p>
                    <input value={val} onChange={(e) => setVal(e.target.value)} className='modal__number modal__number-1' type="number" placeholder='Введіть кількість' />
                </div>
                <div className="modal__miniblock">
                    <p className="modalBold">Одиниці</p>
                    <input className='modal__number modal__number-2' type="text" value={roz} disabled />
                </div>
                <button onClick={() => save()} className="product__btn modal__btn">Зберегти</button>
                <p className={style}>{par}</p>
                <buttom onClick={() => props.setActive(false)} className="modal__close modal__close-2"></buttom>
            </div>
        </div>
    )
}
