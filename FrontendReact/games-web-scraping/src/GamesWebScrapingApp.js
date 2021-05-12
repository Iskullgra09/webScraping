import axios from 'axios';
import React, { useEffect, useState } from 'react'
import ReactPaginate from 'react-paginate';
import Game from './components/Game'
import './styles.css'

const GamesWebScrapingApp = () => {


  //Pagination
  const [loading, setLoading] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [dataPerPage] = useState(7);
  const [data, setData] = useState([]);


  useEffect(() => {
    const fetchData = async () => {
      setLoading(true)

      const res = await axios.get('/getData');

      setData(res.data.data)

      setLoading(false)
    }
    fetchData()
  }, [])


  //Get current photos
  const indexOfLast = currentPage * dataPerPage;
  const indexOfFirst = indexOfLast - dataPerPage;
  const currentData = data.slice(indexOfFirst, indexOfLast);

  //change paginate
  const paginate = (pageNumber) => {
    setCurrentPage(pageNumber.selected + 1)
  };

  //Pagination npm
  const numberOfPages = Math.ceil((data.length) / dataPerPage);

  return (
    <>
      <header>
        <div className="commentBox">
          <ReactPaginate
            initialPage={0}
            previousLabel="<"
            nextLabel=">"
            breakClassName={'break-me'}
            pageCount={numberOfPages}
            marginPagesDisplayed={3}
            pageRangeDisplayed={2}
            onPageChange={paginate}
            containerClassName={'pagination'}
            activeClassName={'active'}
            nextLinkClassName={'pagination-next'}
            previousLinkClassName={'pagination-prev'}
          />
        </div>
      </header>
      <div className="game-container">
        {
          currentData.map((game, index) => (
            <Game key={index} data={game} />
          ))
        }
      </div>
      <footer >
        <div className="autores">
            <span>Carlos Villalobos</span>
            <span>Jason Barrantes</span>
            <span>Isaac Granados</span>
        </div>
      </footer>
    </>
  )
}

export default GamesWebScrapingApp
