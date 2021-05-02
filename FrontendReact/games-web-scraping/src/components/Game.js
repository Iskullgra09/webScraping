import React from 'react'
import srcImg from '../assets/modern-warfare.jpg'

const Game = ({ data }) => {

    console.log('Data', data)
    return (
        <div className="game">
            <img src={data.url} alt="COD" />
            <div className="game-info">
                <h3>Call of Duty: Modern Warfare</h3>
                <span>Precio: 60$</span>
                <span>Puntuación: {data.id}</span>
                <span>Duración: 19h</span>
            </div>
        </div>
    )
}

export default Game
