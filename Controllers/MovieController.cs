using ExampleAPI.Interfaces;
using ExampleAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;
        private readonly IValidator<Movie> _validator;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger, IValidator<Movie> validator)
        {
            _movieService = movieService;
            _logger = logger;
            _validator = validator;
        }

        [HttpPost(Name = "CreateMovie")]
        public ActionResult<Movie> CreateMovie([FromBody] Movie movie)
        {
            var result = _validator.Validate(movie);

            if (!result.IsValid)
            {
                _logger.LogWarning("Invalid movie details provided. Errors: {ValidationErrors}", result.Errors);

                return BadRequest(result.Errors);
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