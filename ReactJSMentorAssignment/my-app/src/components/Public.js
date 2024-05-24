import React from 'react'
import { Outlet } from 'react-router-dom'
import { Navbar } from '.'
import bg from '../assets/bg.png'

const Public = () => {

    return (
        <div className='w-full flex flex-col items-center h-full'
        style ={{ backgroundImage: `url(${bg})`, 
        width:'100%',
        minHeight:'100vh', 
        backgroundSize:'cover',
        backgroundAttachment:'fixed',
        backgroundRepeat:'space'}}
        >
            <Navbar/>
            <div className='w-1100 flex flex-col items-center justify-start'>
                <Outlet/>
            </div>
        </div>
    )
}

export default Public