using ExampleAPI.Models;

namespace ExampleAPI.Interfaces;

public interface IMovieRepository
{
    bool CreateMovie(Movie movie);
    Movie GetMovie(Guid id);
    List<Movie> GetAllMovies();
    bool DeleteMovie(Guid id);
}