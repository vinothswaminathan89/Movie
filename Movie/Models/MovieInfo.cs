using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class MovieInfo
    {
        public string MovieId { get; set; }
        public string Title { get; set; }        
        public string Rated { get; set; }
        public DateTime Released { get; set; }
        public int Runtime { get; set; }        
        public string ImageUrl { get; set; }        
        public List<Session> Sessions { get; set; }
        public int TotalCount { get; set; }
    }

    public class Session
    {
        public int SessionId { get; set; }
        public string MovieId { get; set; }
        public DateTime MovieTime { get; set; }
        public int Seats { get; set; }
    }

    public class TimeOfDay
    {
        public int TimeOfDayId { get; set; }
        public string FilterName { get; set; }
    }

    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }    
}