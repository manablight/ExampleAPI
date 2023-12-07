using ExampleAPI.Interfaces;
using ExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateMovie")]
        public ActionResult<Movie> CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                _logger.LogWarning("Attempted to create a movie with null data");

                return BadRequest(new { message = "Movie cannot be null" });
            }

            var isAddedSuccessfully = _movieService.Create(movie);

            if (!isAddedSuccessfully)
            {
                _logger.LogInformation("Movie already exists. Movie: {@Movie}", movie);

                return Conflict(new { message = "The movie already exists." });
            }

            _logger.LogInformation("Created new movie successfully. Movie: {@Movie}", movie);

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation("Getting movie with Id: {Id}", id);

            var movie = _movieService.GetMovie(id);

            return Ok(movie);
        }

        [HttpGet(Name = "GetAllMovies")]
        public IActionResult GetAllMovies()
        {
            _logger.LogInformation("Getting all movies");

            var movies = _movieService.GetAllMovies();

            return Ok(movies);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteMovie(Guid id)
        {
            _logger.LogInformation("Deleting movie with Id: {Id}", id);

            var isDeletedSuccessfully = _movieService.DeleteMovie(id);

            if (!isDeletedSuccessfully)
            {
                _logger.LogInformation("Movie with Id: {Id} does not exist", id);

                return NotFound(new { message = "The movie does not exist." });
            }

            _logger.LogInformation("Deleted movie with Id: {Id} successfully", id);

            return NoContent();
        }
    }
}