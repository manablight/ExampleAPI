using ExampleAPI.Interfaces;
using ExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService) => _movieService = movieService;


    //These endpoints take the happy path.
    //I'm using an in-memory "database" to keep the example as simple as possible.
    [HttpPost(Name = "CreateMovie")]
    public ActionResult<Movie> CreateMovie([FromBody] Movie movie)
    {
        if (movie == null) return BadRequest(new { message = "Movie cannot be null" });

        var isAddedSuccessfully = _movieService.Create(movie);

        if (!isAddedSuccessfully) return Conflict(new { message = "The movie already exists." });

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var movie = _movieService.GetMovie(id);

        return Ok(movie);
    }

    [HttpGet(Name = "GetAllMovies")]
    public IActionResult GetAllMovies()
    {
        var movies = _movieService.GetAllMovies();

        return Ok(movies);
    }


    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMovie(Guid id)
    {
        var isDeletedSuccessfully = _movieService.DeleteMovie(id);

        if (!isDeletedSuccessfully) return NotFound(new { message = "The movie does not exist." });

        return NoContent();
    }
}