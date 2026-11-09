using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MoviesApi.DTOs;
using System.Collections;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
       public MoviesController()
        {



        }


        // GET api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1,Title = "Movie1" },
                new Movie { Id = 2,Title = "Movie2" }




        };

            return Ok(movies);

        }


        }
}
