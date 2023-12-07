using ExampleAPI.Interfaces;
using ExampleAPI.Models;

namespace ExampleAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies = new();
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(ILogger<MovieRepository> logger)
        {
            _logger = logger;
        }

        public bool CreateMovie(Movie movie)
        {
            try
            {
                if (_movies.Contains(movie)) return false;

                _movies.Add(movie);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occurred while creating a movie: {@Movie}", movie);

                throw new Exception("Error occurred while creating movie", exception);
            }
        }

        public Movie GetMovie(Guid id)
        {
            try
            {
                return _movies.First(movie => movie.Id == id);
            }
            catch (InvalidOperationException exception)
            {
                _logger.LogError(exception, "Movie with given id not found: {Id}", id);

                throw new Exception("Movie with given id not found", exception);
            }
        }

        public List<Movie> GetAllMovies()
        {
            try
            {
                return _movies.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occurred while retrieving all movies");

                throw new Exception("Error occurred while retrieving all movies", exception);
            }
        }

        public bool DeleteMovie(Guid id)
        {
            try
            {
                var movie = _movies.FirstOrDefault(m => m.Id == id);

                if (movie == null) return false;

                _movies.Remove(movie);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occurred while deleting a movie with id: {Id}", id);

                throw new Exception("Error occurred while deleting movie", exception);
            }
        }
    }
}