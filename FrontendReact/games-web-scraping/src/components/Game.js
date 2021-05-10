import React from 'react'

const Game = ({ data }) => {

    return (
        <div className="game">
            <img src={data.imageURL} alt="COD" />
            <div className="game-info">
                <h3>{data.name}</h3>
                <span>{data.price}</span>
                <span>Puntuación: {data.qualification}</span>
                <span>Duración: {data.hltb}</span>
            </div>
        </div>
    )
}

export default Game
