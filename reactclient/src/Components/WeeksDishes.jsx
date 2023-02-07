import React, { useEffect,useState } from 'react'
import w1 from '../img/w1.png'
import w2 from '../img/w2.png'
import w3 from '../img/w3.png'
export default function WeeksDishes() {
    // const [img, setImg] = useState()
    // useEffect(()=>{
    //     fetch('https://drive.google.com/file/d/13sP__raKQDBR5Ef4mpOSpkguK8kc2vO8/view?usp=share_link')
    //     .then(response => response.json())
    //         .then(json => {
    //             setImg(json)
    //         })
    // })
    return (
        <div className="weeks">
            <div className="container">
            <div className='weeks__dishes'>
                <p className='title'>Страви тижня</p>
                <div className="dishes__cards">
                    <div className="dishes__card">
                        <img src={'https://raw.githubusercontent.com/kraw4enko21/int20H__img/main/img/teriyakiSalmonWithHoney.jpg'} alt="weeks" className="dishes__img" />
                        <p className="dishes__time">ЧАС ПРИГОТУВАННЯ: <b>80 хв</b></p>
                        <p className="dishes__title">Лосось теріякі з медом</p>
                    </div>
                    <div className="dishes__card">
                        <img src={'https://raw.githubusercontent.com/kraw4enko21/int20H__img/main/img/Carbonara.jpg'} alt="weeks" className="dishes__img" />
                        <p className="dishes__time">ЧАС ПРИГОТУВАННЯ: <b>60 хв</b></p>
                        <p className="dishes__title">Паста карбонара </p>
                    </div>
                    <div className="dishes__card">
                        <img src='https://raw.githubusercontent.com/kraw4enko21/int20H__img/main/img/Kumpir.jpg' alt="weeks" className="dishes__img" />
                        <p className="dishes__time">ЧАС ПРИГОТУВАННЯ: <b>35 хв</b></p>
                        <p className="dishes__title">Кумпір</p>
                    </div>
                    <div className="dishes__card">
                        <img src='https://raw.githubusercontent.com/kraw4enko21/int20H__img/main/img/Fettuccine%D0%90lfredo.jpg' alt="weeks" className="dishes__img" />
                        <p className="dishes__time">ЧАС ПРИГОТУВАННЯ: <b>80 хв</b></p>
                        <p className="dishes__title">Фетучіні Альфредо</p>
                    </div>
                    <div className="dishes__card">
                        <img src='https://raw.githubusercontent.com/kraw4enko21/int20H__img/main/img/pastaBoloniese.jpg' alt="weeks" className="dishes__img" />
                        <p className="dishes__time">ЧАС ПРИГОТУВАННЯ: <b>15 хв</b></p>
                        <p className="dishes__title">Спагетті болоньєзе</p>
                    </div>
                </div>
            </div>
        </div>

        </div>
    )
}
