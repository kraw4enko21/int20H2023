import {React, useState, useEffect} from 'react'
import { globalUrl } from './url';
export default function Ingregient(props) {
    const imageUrl = globalUrl+`/getImage?path=${props.info.product.image}`
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
    function newFun() {
        switch (props.info.product.measurmentType) {
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
        <div className="ingredients__card">
            <img src={img} alt="ava" className="ingredients__img" />
            <p className="ingredients__title">{props.info.product.title} <br></br> <b>{props.info.quantity} {newFun()}</b></p>
        </div>
    )
}
