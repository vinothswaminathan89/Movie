using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Movie.Models;
using System.Collections.Generic;
using System.Data;
using System;
using System.Linq;

namespace Movie.DAL
{
    public class Movie
    {
        private const string ConnectionString = "movieDB";
        private Database db = null;

        public Movie()
        {
            var factory = new DatabaseProviderFactory();
            db = factory.Create(ConnectionString);
        }

        public List<TimeOfDay> GetTimeOfDay()
        {
            var result = new List<TimeOfDay>();            
            var sqlCommand = "proc_GetTimeOfDay";
            using (var dbCommand = db.GetStoredProcCommand(sqlCommand))
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        result.Add(
                            new TimeOfDay
                            {
                                TimeOfDayId = ConvertValue<int>(dataReader, "TimeOfDayId", -1),
                                FilterName = ConvertValue<string>(dataReader, "FilterName", string.Empty)
                            }
                        );
                    }
                }
            }
            return result;
        }

        public List<Genre> GetGenre()
        {
            var result = new List<Genre>();
            var sqlCommand = "proc_GetGenre";
            using (var dbCommand = db.GetStoredProcCommand(sqlCommand))
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        result.Add(
                            new Genre
                            {
                                GenreId = ConvertValue<int>(dataReader, "GenreId", -1),
                                GenreName = ConvertValue<string>(dataReader, "GenreName", string.Empty)
                            }
                        );
                    }
                }
            }
            return result;
        }

        public List<Session> GetSessionInfo(DateTime movieDate)
        {
            var result = new List<Session>();
            var sqlCommand = "proc_GetSessionInfo";
            using (var dbCommand = db.GetStoredProcCommand(sqlCommand))
            {
                db.AddInParameter(dbCommand, "@MovieDate", DbType.DateTime, movieDate);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        result.Add(
                            new Session
                            {
                                MovieId = ConvertValue<string>(dataReader, "MovieId", -1),
                                SessionId = ConvertValue<int>(dataReader, "SessionId", -1),
                                Seats = ConvertValue<int>(dataReader, "Seats", -1),
                                MovieTime = ConvertValue<DateTime>(dataReader, "MovieTime", DateTime.MinValue),
                            }
                        );
                    }
                }
            }
            return result;
        }
       
        public List<MovieInfo> GetMoviesInfo(DateTime movieDate, string timeOfDayId, string genreId, int startIndex, int endIndex)
        {
            var result = new List<MovieInfo>();
            var sqlCommand = "proc_GetMoviesInfo";
            var sessions = GetSessionInfo(movieDate);           
            using (var dbCommand = db.GetStoredProcCommand(sqlCommand))
            {
                db.AddInParameter(dbCommand, "@MovieDate", DbType.DateTime, movieDate);
                db.AddInParameter(dbCommand, "@TimeOfDayId", DbType.String, timeOfDayId);
                db.AddInParameter(dbCommand, "@GenreId", DbType.String, genreId);
                db.AddInParameter(dbCommand, "@StartIndex", DbType.Int32, startIndex);
                db.AddInParameter(dbCommand, "@EndIndex", DbType.Int32, endIndex);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        var movieId = ConvertValue<string>(dataReader, "MovieId", -1);
                        result.Add(
                            new MovieInfo
                            {
                                MovieId = movieId,
                                Title = ConvertValue<string>(dataReader, "Title", string.Empty),
                                Rated = ConvertValue<string>(dataReader, "Rated", string.Empty),
                                Released = ConvertValue<DateTime>(dataReader, "Released", DateTime.MinValue),
                                Runtime = ConvertValue<int>(dataReader, "Runtime", 0),
                                ImageUrl = ConvertValue<string>(dataReader, "ImageUrl", string.Empty),                                
                                Sessions = sessions.Where(s => s.MovieId == movieId).ToList(),
                                TotalCount = ConvertValue<int>(dataReader, "TotalCount", 0),
                            }
                        );
                    }                    
                }
            }
            return result;
        }

        private T ConvertValue<T>(IDataReader dataReader, string columnName, object defaultValue)
        {
            if (dataReader != null && !string.IsNullOrEmpty(columnName))
            {
                var value = dataReader[columnName];
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    return (T)value;
                }
            }
            return (T)defaultValue;
        }
    }
}