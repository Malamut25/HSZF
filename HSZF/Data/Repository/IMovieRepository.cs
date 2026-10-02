using HSZF.Data.Entities;

namespace HSZF.Data.Repository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> Movies { get; }

        void create(Movie movie);
        void delete(Movie movie);
        void updatable(Movie movie);
        void Load(string path);
        void Save(string path);
    }
}