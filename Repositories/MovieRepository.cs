using ExampleAPI.Models;

namespace ExampleAPI.Repositories
{
    public class MovieRepository
    {
        private readonly List<Movie> _movies = new();

        public bool CreateMovie(Movie movie)
        {
            try
            {
                if (_movies.Contains(movie)) return false;

                _movies.Add(movie);

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while creating movie");
            }
        }

        public Movie GetMovie(Guid id)
        {
            try
            {
                return _movies.First(movie => movie.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Movie with given id not found");
            }
        }

        public List<Movie> GetAllMovies()
        {
            try
            {
                return _movies.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while retrieving all movies");
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
            catch (Exception)
            {
                throw new Exception("Error occurred while deleting movie");
            }
        }
    }
}