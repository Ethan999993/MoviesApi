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
            var movies = await _db.QueryAsync<Movie,Genre,AudienceRating,Movie>(
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


        }
}
