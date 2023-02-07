import React, { useState,useEffect } from 'react'
import { globalUrl } from './url'
export default function Filters(props) {
    const [nash, setNath]=useState('')
    const [user, setUser]=useState(false)
    const [id,setId]=useState('')
    const [comp, setComp] = useState(0)
    const [time, setTime] = useState(' ')
    useEffect(() => {
        fetch(globalUrl + '/users')
            .then(response => response.json())
            .then(json => setId(json[0].id))
    })
    console.log(id)
    function filter()
    {
        let localOne = ''
        if(nash!=='')
        {
            localOne = 'filterNarionality='+nash+'&'
        }
        let localTwo = ''
        if(user)
        {
            localOne = 'userId='+id+'&'
        }
        let localThree = ''
        if(comp!==0)
        {
            localThree = 'filterComplaxity='+comp+'&'
        }
        let localFour = ''
        if(time!=='')
        {
            if(time==='time1')
            {
                localFour='filterMinTime=0&filterMaxTime=30&'
            }
            else if(time==='time2')
            {
                localFour='filterMinTime=30&filterMaxTime=60&'
            }
            else if(time==='time3')
            {
                localFour='filterMinTime=60&'
            }
            
        }
        const localUrl = globalUrl+'/dishes?'+localOne+localTwo+localThree+localFour
        fetch(localUrl)
            .then(response => response.json())
            .then(json => props.setAllDishes(json))
    }
    return (
        <div>
            <p className="title">Фільтри</p>
            <p className="filter__title">Кухня:</p>
            <div className="radio__block">
                <input onChange={e=>setNath(e.target.value)} className='radio__btn' type="radio" id="kitchen1"
                    name="kitchen" value="Турецька" />
                <label className='radio__label' for="kitchen1">Турецька</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setNath(e.target.value)} className='radio__btn' type="radio" id="kitchen2"
                    name="kitchen" value="Іспанська" />
                <label className='radio__label' for="kitchen2">Іспанська</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setNath(e.target.value)} className='radio__btn' type="radio" id="kitchen3"
                    name="kitchen" value="Польська" />
                <label className='radio__label' for="kitchen3">Польська</label>
            </div>
            <p className="filter__title">Приготувати з моїх продуктів:</p>
            <div className="radio__block">
                <input onChange={e=>setUser(e.target.value)} className='radio__btn' type="radio" id="my1"
                    name="my" value="true" />
                <label className='radio__label' for="my1">Так</label>
            </div>
            <div className="radio__block">
                <input className='radio__btn' type="radio" id="my2"
                    name="my" value="false" />
                <label className='radio__label' for="my2">Ні</label>
            </div>
            <p className="filter__title">Складність приготування:</p>
            <div className="radio__block">
                <input onChange={e=>setComp(e.target.value)} className='radio__btn' type="radio" id="complexity1"
                    name="complexity" value="1" />
                <label className='radio__label' for="complexity1">1</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setComp(e.target.value)} className='radio__btn' type="radio" id="complexity2"
                    name="complexity" value="2" />
                <label className='radio__label' for="complexity2">2</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setComp(e.target.value)} className='radio__btn' type="radio" id="complexity3"
                    name="complexity" value="3" />
                <label className='radio__label' for="complexity3">3</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setComp(e.target.value)} className='radio__btn' type="radio" id="complexity4"
                    name="complexity" value="4" />
                <label className='radio__label' for="complexity4">4</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setComp(e.target.value)} className='radio__btn' type="radio" id="complexity5"
                    name="complexity" value="5" />
                <label className='radio__label' for="complexity5">5</label>
            </div>
            <p className="filter__title">Час приготування:</p>
            <div className="radio__block">
                <input onChange={e=>setTime('time1')} className='radio__btn' type="radio" id="time1"
                    name="time" value="1" />
                <label className='radio__label' for="time1">{'<'} 30 хв</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setTime('time2')} className='radio__btn' type="radio" id="time2"
                    name="time" value="2" />
                <label className='radio__label' for="time2">30 хв - 1 год</label>
            </div>
            <div className="radio__block">
                <input onChange={e=>setTime('time3')} className='radio__btn' type="radio" id="time3"
                    name="time" value="3" />
                <label className='radio__label' for="time3"> {'>'} 1 год</label>
            </div>
            <button onClick={()=> filter()} className="dishes__btn filter__btn" >Відфільтрувати</button>
        </div>
    )
}
