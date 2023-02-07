import {React,useState,useEffect} from 'react'
import Ingregient from './Ingregient';

export default function ModalDishes(props) {
    let recipe = props.info.recipe.split('\r\n')
    function ing() {
        return props.info.products.map((i) => {
            return <Ingregient info={i}/>
        })
    }
    function showRecipe()
    {
        return recipe.map((i,n)=><p className="recipe__step"><b>КРОК {n+1}  </b> {i}</p>)
    }
    return (
        <div className={props.active ? 'modaldishes active  ' : 'modaldishes '} onClick={() => props.setActive(false)}>
            <div className={props.active ? 'modaldishes__content active  ' : 'modaldishes__content '} onClick={e => e.stopPropagation()}>
                <p className="title">{props.info.title}</p>
                <p className="dishes__description">{props.info.descriprion}</p>
                <div className="modal__block">
                    <div className="leftcolomp">
                        <img src={props.ava} alt="Dishes" className="modal__ava" />
                        <p className="modal__time modal__time-1">ЧАС <br></br> ПРИГОТУВАННЯ: <br></br><b>{props.info.cookingTime} хв</b></p>
                        <p className="modal__time">СКЛАДНІСТЬ <br></br> ПРИГОТУВАННЯ: <br></br><b>{props.info.complexity}</b></p>
                        <div className="dishes__raiting newClass">
                            <button className="dishes__button dishes__dec new">-</button>
                            <p>{props.info.rate}</p>
                            <button className="dishes__button dishes__inc">+</button>
                        </div>
                    </div>
                    <div className="rightcolomp">
                        <p className="title">Інгредієнти</p>
                        <div className="ingredients">
                            {
                                ing()
                            }
                        </div>
                    </div>
                </div>
                <div className="recipe">
                    <p className="title">Рецепт</p>
                    {
                        showRecipe()
                    }

                </div>
                <buttom onClick={() => props.setActive(false)} className="modal__close"></buttom>
            </div>
        </div>
    )
}
