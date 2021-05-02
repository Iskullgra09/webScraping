import React, { useEffect, useState } from 'react'
import Game from './components/Game'
import './styles.css'

const GamesWebScrapingApp = () => {

  const [games, setGames] = useState(['1', '2', '3', '4', '5', '6', '7', '8', '9']);

  useEffect(() => {
    //fetch real data
  }, []);


  return (
    <div className="game-container">
      {
        games.map((game, index) => (
          <Game key={index} />
        ))
      }
    </div>
  )
}

export default GamesWebScrapingApp
