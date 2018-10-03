using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.Controllers
{
    [RoutePrefix("api/v1/movies")]
    public class MovieController : ApiController
    {
        IBLL.IMovie objBLL;
        public MovieController()
        {
            objBLL = new BLL.Movie();
        }

        #region HttpGet

        [HttpGet]
        [Route("filters/timeOfDay")]
        public List<TimeOfDay> GetTimeOfDay()
        {
            return objBLL.GetTimeOfDay();
        }

        [HttpGet]
        [Route("filters/genre")]
        public List<Genre> GetGenre()
        {
            return objBLL.GetGenre();
        }

        [HttpGet]
        [Route("Session")]
        public List<Session> GetSessionInfo(DateTime movieDate)
        {
            return objBLL.GetSessionInfo(movieDate);
        }

        [HttpGet]
        [Route("")]
        public List<MovieInfo> GetMoviesInfo(DateTime movieDate, string timeOfDayId, string genreId, int startIndex, int endIndex)
        {
            return objBLL.GetMoviesInfo(movieDate, timeOfDayId, genreId, startIndex, endIndex);
        }

        #endregion
    }
}
