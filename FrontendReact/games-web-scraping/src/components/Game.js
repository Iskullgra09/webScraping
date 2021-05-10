import React from 'react'

const Game = ({ data }) => {

    return (
        <div className="game">
            <img src={data.imageUrl} alt="COD" />
            <div className="game-info">
                <h3>{data.name}</h3>
                <span>{data.price}</span>
                <span>Puntuación: {data.score}</span>
                <span>Duración: {data.timeToBeat}</span>
            </div>
        </div>
    )
}

export default Game
