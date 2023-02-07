import {React, useState,useEffect} from 'react'
import ModalDishes from './ModalDishes'
import { globalUrl } from './url';
export default function Dishe(props) {
    const [modalActive, setModalActive] = useState(false)
    const imageUrl = globalUrl+`/getImage?path=${props.info.image}`;
    const [img, setImg] = useState();

    const fetchImage = async () => {
      const res = await fetch(imageUrl);
      const imageBlob = await res.blob();
      const imageObjectURL = URL.createObjectURL(imageBlob);
      setImg(imageObjectURL);
    };
  
    useEffect(() => {
      fetchImage();
    }, []);
    function asd()
    {
        if(props.info.descriprion.length>=180)
        {
            return props.info.descriprion.slice(0,180)+'...'
        }
        else
        {
            return props.info.descriprion
        }
    }
    return (
        <div className="dishes__block">
            <img src={img} alt="Dishes" className="dishes__ava" />
            <div className="dishes__miniblock">
                <p className="dishes__time">ЧАС ПРИГОТУВАННЯ: <b>{props.info.cookingTime} хв</b></p>
                <p className="dishes__time">СКЛАДНІСТЬ: <b>{props.info.complexity}</b></p>
                <p className="dishes__title">{props.info.title}</p>
                <p className="dishes__description">{asd()}</p>
                <div className="dishes__btns">
                    <button className="dishes__btn" onClick={()=> setModalActive(true)}>Розкрити рецепт</button>
                    <div className="dishes__raiting">
                        <button className="dishes__button dishes__dec">-</button>
                        <p>{props.info.rate}</p>
                        <button className="dishes__button dishes__inc">+</button>
                    </div>
                </div>
            </div>
            <ModalDishes active={modalActive} setActive={setModalActive} info={props.info} ava={img} />
        </div>
    )
}
