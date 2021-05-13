import React from 'react'
import './Game.css'

const Game = ({ data }) => {

    return (
        <div className="game">
            {
                data.offer ? (
                    <div className="ajuste">
                    <span className="popular-badge">
                    <span>OFERTA</span>
                    </span>
                    </div>
                ) : null
            }
            <img src={data.imageURL} alt="COD" />
            <div className="game-info">
                <div className="name">
                    <p>{data.name}</p>
                </div>
                <span>{data.price}</span>
                <span>Puntuación: {data.qualification}</span>
                <span>Duración: {data.hltb}</span>
            </div>
        </div>
    )
}

export default Game
