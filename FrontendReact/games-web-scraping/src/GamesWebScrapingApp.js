import axios from 'axios';
import React, { useEffect, useState } from 'react'
import ReactPaginate from 'react-paginate';
import Game from './components/Game'
import './styles.css'

const GamesWebScrapingApp = () => {

  // const [games, setGames] = useState(['1', '2', '3', '4', '5', '6', '7', '8', '9']);

  //Pagination
  const [photos, setPhotos] = useState([])
  const [loading, setLoading] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [photosPerPage] = useState(20);

  useEffect(() => {
    //fetch example
    const fetchPhotos = async () => {
      setLoading(true);

      const res = await axios.get('https://jsonplaceholder.typicode.com/photos');
      setPhotos(res.data);

      setLoading(false);

    }

    fetchPhotos();
    //fetch real data
  }, []);


  //Get current photos
  const indexOfLastPhoto = currentPage * photosPerPage;
  const indexOfFirstPhoto = indexOfLastPhoto - photosPerPage;
  const currentPhotos = photos.slice(indexOfFirstPhoto, indexOfLastPhoto);

  //change paginate
  const paginate = (pageNumber) => {
    setCurrentPage(pageNumber.selected + 1)
  };

  //Pagination npm
  const numberOfPages = Math.ceil((photos.length) / photosPerPage);

  return (
    <>
      <header>
        <div className="commentBox">
          <ReactPaginate
            initialPage={1}
            previousLabel="<"
            nextLabel=">"
            breakClassName={'break-me'}
            pageCount={numberOfPages}
            marginPagesDisplayed={2}
            pageRangeDisplayed={3}
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
          currentPhotos.map((game, index) => (
            <Game key={index} data={game} />
          ))
        }
      </div>
    </>
  )
}

export default GamesWebScrapingApp
