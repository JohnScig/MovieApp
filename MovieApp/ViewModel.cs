using Data;
using MovieConnector.Models;
using System;
using System.Collections.Generic;

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
        public MovieDetail CurrentMovie { get; private set; }

        /// <summary>
        /// Collection of all loaded movies.
        /// </summary>
        public IReadOnlyCollection<MovieListResult> Movies { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Ctor.
        /// </summary>
        public ViewModel()
        {
            _repository = new MovieRepository();
        }

        #endregion

        #region Movie Loading

        /* YOUR MAGIC HERE */

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