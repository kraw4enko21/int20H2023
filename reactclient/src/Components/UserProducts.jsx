import { React, useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import ModalProduct from './ModalProduct'
import Products from './Products'
import { globalUrl } from './url'
import empty from '../img/empty.png'
export default function UserProducts(props) {
    const [modalActive, setModalActive] = useState(false)
    const [userId, setUserId] = useState()
    const [general, setGeneral] = useState([])
    const [res, setRes] = useState()
    useEffect(() => {
        fetch(globalUrl + '/users')
            .then(response => response.json())
            .then(json => {
                setUserId(json[0].id)
                if (json[0].products.length !== 0) {
                    
                    fetch(`${globalUrl}/getGrouperUsersProduct?userId=${json[0].id}`)
                        .then(response => response.json())
                        .then(json => {
                            let localArray = []
                            for (let i in json.items) {
                                let local = {
                                    title: json.items[i].productCategories.title,
                                    products: []
                                }
                                local.products = json.items[i].products.map((i) => {
                                    let localTwo = {
                                        id: i.product.id,
                                        title: i.product.title,
                                        quantity: i.quantity,
                                        type: i.product.measurmentType, 
                                        url: globalUrl + `/getImage?path=${i.product.image}`,
                                    }

                                    return localTwo

                                })
                                localArray[i] = local

                            }
                            setGeneral(localArray)
                        })
                    
                }
            })
    }, [res])
    function showProduct() {
        if(general.length===0)
        {
            return <div className="nullproduct">
                <img src={empty} alt="empty" />
                <p>У вашому списку інгрідієнтів пусто!<br></br>
                     Почність додавати продукти<br></br>
                      для пошуку рецептів</p>
            </div>
        }
        else
        {
            return <div>
                {general.map((i,n)=> <Products setRes={setRes} id={userId} key={n} info={i}  />)}
                <NavLink onClick={()=>newFun()} className="product__dishes" to='/'>Відобразити страви <br></br> з цих ігрідієнтів</NavLink>

            </div>
                
            
        }
        
    }
    console.log(res)
    function newFun()
    {
        fetch(globalUrl + '/dishes?filterComplaxity=5&userId='+userId)
            .then(response => response.json())
            .then(json => props.setAllDishes(json))
    }
    console.log(general)
    return (
        <div className='product'>
            <div className="container">
                <div className="product__header">
                    <p className="title">Мої продукти</p>
                    <button onClick={() => setModalActive(true)} className="product__btn">Додати<br></br> продукт</button>
                </div>
                {
                    showProduct()
                }
               
            </div>
            <ModalProduct  setRes={setRes} id={userId} active={modalActive} setActive={setModalActive} />
        </div>
    )
}
