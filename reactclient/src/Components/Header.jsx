import React from 'react'
import { NavLink } from 'react-router-dom'
import logo from '../img/LOGO.png'
export default function Header(props) {
    return (
        <div className="header__main">
            <div className="container">
            <div className='header'>
                <img className='header__logo' src={logo} alt="logo" />
                <div className="menu">
                    <NavLink className={({ isActive }) => isActive ? 'menu__link menu__link-active' : 'menu__link'}
                        to='/' >
                        Список страв
                    </NavLink>
                    <NavLink className={({ isActive }) => isActive ? 'menu__link menu__link-active' : 'menu__link'}
                        to='2' >
                        Мої продукти
                    </NavLink>
                </div>
            </div>
        </div>
        </div>

    )
}
