import React from 'react'
import srcImg from '../assets/modern-warfare.jpg'

const Game = () => (
    <div className="game">
        <img src={srcImg} alt="COD" />
        <div className="game-info">
            <h5>Call of Duty: Modern Warfare</h5>
            <span>Precio: 60$</span>
            <span>Puntuacion: 9.5</span>
            <span>Duracion: 19h</span>
        </div>
    </div>
)

export default Game
