using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.IBLL
{
    public interface IMovie
    {
        List<TimeOfDay> GetTimeOfDay();

        List<Genre> GetGenre();

        List<Session> GetSessionInfo(DateTime movieDate);

        List<MovieInfo> GetMoviesInfo(DateTime movieDate, string timeOfDayId, string genreId, int startIndex, int endIndex);
    }
}
