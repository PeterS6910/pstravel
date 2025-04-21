import { useState } from 'react'
import './navbar.css'
import { AiFillCloseCircle } from 'react-icons/ai/index.js'
import { TbGridDots } from 'react-icons/tb/index.js'
import { MdOutlineTravelExplore } from 'react-icons/md/index.js'

const Navbar = () => {

    const [active, setActive] = useState('navBar')
    const showNav = () => {
        setActive('navBar activeNavbar')
    }
    const removeNav = () => {
        setActive('navBar')
    }
    return (
        <section className='navBarSection'>
            <header className="header flex">

                <div className="logoDiv">
                    <a href="#" className="logo flex"><h1><MdOutlineTravelExplore className='icon' />  Travel.</h1></a>
                </div>

                <div className={active}>
                    <ul onClick={removeNav} className="navLists flex">
                        <li className="navItem">
                            <a href="#" className="navLink">Home</a>
                        </li>
                        <li className="navItem">
                            <a href="#" className="navLink">Packages</a>
                        </li>
                        <li className="navItem">
                            <a href="#" className="navLink">Shop</a>
                        </li>
                        <li className="navItem">
                            <a href="#" className="navLink">About </a>
                        </li>
                        <li className="navItem">
                            <a href="#" className="navLink">Pages</a>
                        </li>
                        <li className="navItem">
                            <a href="#" className="navLink">News</a>
                        </li>
                        <li className="navItem">
                            <a href="#" className="navLink">Contact</a>
                        </li>
                        <button className="btn">
                            <a href="#">BOOK NOW</a>
                        </button>

                    </ul>
                    <div onClick={removeNav} className="closeNavbar">
                        <AiFillCloseCircle className='icon' />
                    </div>
                </div>

                <div onClick={showNav} className="toggleNavbar">
                    <TbGridDots className='icon' />
                </div>
            </header>
        </section>
    )
}

export default Navbar
