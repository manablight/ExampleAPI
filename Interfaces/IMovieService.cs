using ExampleAPI.Models;

namespace ExampleAPI.Interfaces;

public interface IMovieService
{
    bool Create(Movie movie);
    Movie GetMovie(Guid id);
    List<Movie> GetAllMovies();
    bool DeleteMovie(Guid id);
}