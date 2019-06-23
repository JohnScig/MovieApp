using System.Threading.Tasks;
using MovieConnector;
using MovieConnector.Models;
using MovieConnector.Services;

namespace Data
{
    /// <summary>
    /// Repository for obtaining movie data from service.
    /// </summary>
    public class MovieRepository
    {
        /// <summary>
        /// Service for obtaining data.
        /// </summary>
        private MovieService _movieService = new MovieService();

        /// <summary>
        /// Gets all available movie genres.
        /// </summary>
        /// <returns>Movie genres.</returns>
        public DataResponse<GenreList> GetAvailableGenres() =>
            Task.Run(async () => await _movieService.GetAvailableGenres()).Result;

        /// <summary>
        /// Gets page of 20 top rated movies.
        /// </summary>
        /// <returns>Page of top rated movies.</returns>
        public DataResponse<PageResult<MovieListResult>> GetTopRatedMovies() =>
            Task.Run(async () => await _movieService.GetTopRatedMovies()).Result;

        /// <summary>
        /// Gets page with 20 of top rated movies.
        /// </summary>
        /// <param name="page">Result page.</param>
        /// <returns>Page with 20 of top rated movies.</returns>
        public DataResponse<PageResult<MovieListResult>> GetTopRatedMovies(int page) =>
            Task.Run(async () => await _movieService.GetTopRatedMovies(page)).Result;

        /// <summary>
        /// Gets page of 20 movies by genre.
        /// </summary>
        /// <param name="genreId">Genre ID.</param>
        /// <returns>Page of movies by genre.</returns>
        public DataResponse<PageResult<MovieListResult>> GetMoviesByGenre(int genreId) =>
            Task.Run(async () => await _movieService.GetMoviesByGenre(genreId)).Result;

        /// <summary>
        /// Gets page with 20 of movies by genre.
        /// </summary>
        /// <param name="genreId">Genre ID.</param>
        /// <param name="page">Result page.</param>
        /// <returns>Page with 20 of movies by genre.</returns>
        public DataResponse<PageResult<MovieListResult>> GetMoviesByGenre(int genreId, int page) =>
            Task.Run(async () => await _movieService.GetMoviesByGenre(genreId, page)).Result;

        /// <summary>
        /// Gets movie detail.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        /// <returns>Movie detail.</returns>
        public DataResponse<MovieDetail> GetMovieDetail(int movieId) =>
            Task.Run(async () => await _movieService.GetMovieDetail(movieId)).Result;

    }
}
