using System;
using System.Collections.Generic;
using Movie.Models;

namespace Movie.BLL
{
    public class Movie : IBLL.IMovie
    {
        private DAL.Movie objDAL = null;
        public Movie()
        {
            objDAL = new DAL.Movie();
        }

        public List<TimeOfDay> GetTimeOfDay()
        {
            try
            {
                return objDAL.GetTimeOfDay();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Genre> GetGenre()
        {
            try
            {
                return objDAL.GetGenre();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Session> GetSessionInfo(DateTime movieDate)
        {
            try
            {
                return objDAL.GetSessionInfo(movieDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MovieInfo> GetMoviesInfo(DateTime movieDate, string timeOfDayId, string genreId, int startIndex, int endIndex)
        {
            try
            {
                return objDAL.GetMoviesInfo(movieDate, timeOfDayId, genreId, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}