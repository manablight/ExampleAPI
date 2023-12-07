using ExampleAPI.Models;
using ExampleAPI.Repositories;

namespace ExampleAPI.Services;

public class MovieService
{
    private readonly MovieRepository _movieRepository;

    public MovieService(MovieRepository movieRepository) => _movieRepository = movieRepository;

    public bool Create(Movie movie) => _movieRepository.CreateMovie(movie);
    public Movie GetMovie(Guid id) => _movieRepository.GetMovie(id);
    public List<Movie> GetAllMovies() => _movieRepository.GetAllMovies();
    public bool DeleteMovie(Guid id) => _movieRepository.DeleteMovie(id);
}