using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MoviesApi.DTOs;
using System.Collections;
using System.Data;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IDbConnection _db;
        public MoviesController(IDbConnection db)
        {

            _db = db;

        }

       


        

        //@select m.*, g.*, a.*, r.*, u.*
        //        from Movie m
        //        Inner JOIN Genre g  On m.GenreId = g.Id
        //        Inner JOIN AudienceRating a  On m.AudienceRatingId = a.Id
        //        left outer join CriticReview r on m.id = r.MovieId
        //        left outer join UserSnapshot u on r.CreatedBy = u.UserId
        //where m.id = 6004



        // GET api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            //fetch all movies from databse 



            var sql = @"select m.*, g.*, a.*
                from Movie m
                Inner JOIN Genre g  On m.GenreId = g.Id
                Inner JOIN AudienceRating a  On m.AudienceRatingId = a.Id"
;


            //fetch all movies from databse  <table table 2 output>
            var movies = await _db.QueryAsync<Movie, Genre, AudienceRating, Movie>(
                sql,
                (movie, genre, AudienceRating) =>
                {
                    movie.Genre = genre;
                    movie.AudienceRating = AudienceRating;
                    return movie;
                }

                );

            return Ok(movies);

        }

        // get /api/movies/id

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieById(int id)
        {
            //fetch all movies from databse 
            var sql = @"select m.*, g.*, a.*, r.*, u.*
                from Movie m
                Inner JOIN Genre g  On m.GenreId = g.Id
                Inner JOIN AudienceRating a  On m.AudienceRatingId = a.Id
                left outer join CriticReview r on m.id = r.MovieId
                left outer join UserSnapshot u on r.CreatedBy = u.UserId
                Where m.id = @Id";

                Movie? movie = null;
            //fetch all movies from databse  <table table 2 output>
            await _db.QueryAsync<Movie, Genre, AudienceRating, Review, User, Movie>(
                sql,
                (m, g, a, r, u) =>
                {
                    if (movie == null)
                    {
                        movie = m;
                        movie.Genre = g;
                        movie.AudienceRating = a;
                        movie.Reviews = new List<Review>(); // initialize an empty list
                    }

                    if (r != null)
                    {
                        r.User = u;
                        movie.Reviews.Add(r);
                    }

                    return movie;
                },
                new {Id = id},
                splitOn:"Id,Id,Id,UserId"

                );

            return Ok(movie);

        }








    }
}
