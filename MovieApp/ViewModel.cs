using Data;
using MovieConnector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieApp
{
    /// <summary>
    /// View model for Movie App.
    /// </summary>
    class ViewModel
    {
        #region Enumerates

        /// <summary>
        /// Watch choice preference enum.
        /// </summary>
        public enum eWatchChoice
        {
            None = 0,
            Seen = 1,
            Want = 2,
            NotInterested = 3
        }

        #endregion

        #region Events

        /// <summary>
        /// Handler for data load events.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event parameters.</param>
        public delegate void DataLoadHandler(object sender, EventArgs e);
        /// <summary>
        /// Event that occurs when data start to load.
        /// </summary>
        public event DataLoadHandler DataLoadStart;
        /// <summary>
        /// Event that occurs when data finished loading.
        /// </summary>
        public event DataLoadHandler DataLoadFinish;

        #endregion

        #region Attributes

        /// <summary>
        /// Repository for obtaining movies.
        /// </summary>
        private MovieRepository _repository;

        #endregion

        #region Properties

        /// <summary>
        /// Currently selected movie.
        /// </summary>
        public MovieDetail CurrentMovie {get; set;}
        //public string Title { get => CurrentMovie?.Title; }
        //public string Tagline { get => CurrentMovie?.Tagline; }
        //public string IMDBLink { get => $"<image=IMDB.png><href>https://www.imdb.com/title/{CurrentMovie.ImdbId}</href>"; }
        public int Index {get; set;}
        /// <summary>
        /// Collection of all loaded movies.
        /// </summary>
        public IReadOnlyCollection<MovieListResult> Movies { get; private set; }

        public HashSet<string> FavoriteMovies { get; set; }
        public List<Genre> Genres { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Ctor.
        /// </summary>
        public ViewModel()
        {
            _repository = new MovieRepository();
            FavoriteMovies = new HashSet<string>();
            //Genres = new List<GenreList>();
        }

        #endregion

        #region Movie Loading

        public void LoadGenres()
        {
            Genres = _repository.GetAvailableGenres().Data.Genres.ToList();
        }

        public void LoadTopRatedMovies()
        {
            var movies = _repository.GetTopRatedMovies();
            Movies = new List<MovieListResult> (movies.Data.Results);
            if (Movies.Any())
            {
                LoadCurrentMovieDetail(Movies.ElementAt(Index).Id);
            }
        }

        internal void LoadGenreMovies(string selectedValue)
        {
            var movies = _repository.GetMoviesByGenre(Genres.Find(c=>c.Name==selectedValue).Id);
            Movies = new List<MovieListResult>(movies.Data.Results);
            if (Movies.Any())
            {
                LoadCurrentMovieDetail(Movies.ElementAt(Index).Id);
            }
        }

        public void LoadNextMovie()
        {
            if (Index == Movies.Count-1)
            { }
            else
            LoadCurrentMovieDetail(Movies.ElementAt(++Index).Id);
        }



        public void LoadPreviousMovie()
        {
            if (Index == 0)
            { }
            else
                LoadCurrentMovieDetail(Movies.ElementAt(--Index).Id);
        }

        public void LoadCurrentMovieDetail(int movieId)
        {
            CurrentMovie = new MovieDetail()
            {
                Title = (Movies.First(c=>c.Id==movieId).Title),
                Tagline = (Movies.First(c => c.Id == movieId).Overview),
                ImdbId = (Movies.First(c => c.Id == movieId).Id).ToString(),
                PosterPath = Movies.First(c => c.Id == movieId).PosterPath,
                ReleaseDate = Movies.First(c => c.Id == movieId).ReleaseDate,
                VoteAverage = Movies.First(c => c.Id == movieId).VoteAverage
            };
            //CurrentMovie = _repository.GetMovieDetail(movieId).Data;
            //InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Title)));
            //InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Tagline)));
            //InvokePropertyChanged(new PropertyChangedEventArgs(nameof(IMDBLink)));
        }

        #endregion

        #region Movie Selection

        /// <summary>
        /// Selects previous movie from list.
        /// </summary>
        public void SelectPreviousMovie()
        {
            /* INSERT YOUR MAGIC HERE */
        }

        /// <summary>
        /// Selects next movie from list.
        /// </summary>
        public void SelectNextMovie()
        {
            /* INSERT YOUR MAGIC HERE */
        }

        #endregion

        #region I/O

        /// <summary>
        /// Saves text to file.
        /// </summary>
        /// <param name="text">Text to save.</param>
        /// <param name="fileName">Filename.</param>
        /// <returns>True, if saving was successful; otherwise false.</returns>
        private bool SaveTextToFile(string text, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    sw.WriteLine(text);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal void SaveToFile(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Movies");
            sb.AppendLine("-----------------");
            foreach (var item in Movies)
            {
                sb.AppendLine($"{item.Id}: {item.Title}");
            }

            sb.AppendLine("Favorites");
            sb.AppendLine("-----------------");
            foreach (var item in FavoriteMovies)
            {
                sb.AppendLine(item);
            }

            SaveTextToFile(sb.ToString(), path+"\\movies.txt");
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Raises DataLoadStart event.
        /// </summary>
        /// <param name="e">Event parameters.</param>
        private void OnDataLoadStart(EventArgs e)
        {
            DataLoadStart?.Invoke(this, e);
        }

        /// <summary>
        /// Raises DataLoadFinish event.
        /// </summary>
        /// <param name="e">Event parameters.</param>
        private void OnDataLoadFinish(EventArgs e)
        {
            DataLoadFinish?.Invoke(this, e);
        }

        #endregion

    }
}