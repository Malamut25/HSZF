using HSZF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HSZF.Data.Repository
{
    internal class MovieRepository : IMovieRepository
    {
        JsonSerializerOptions options;
        MovieContext context;
        public MovieRepository()
        {
            options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                IncludeFields = false,
                MaxDepth = 1024
            };
            context = new MovieContext();
        }

        public IEnumerable<Movie> Movies { get => context.Movies; }


        public void updatable(Movie movie)
        {
            var q = context.Movies.First(x => x.Id == movie.Id);
            if (q != null)
            {
                q.Actors = movie.Actors;
                q.Roles = movie.Roles;
                q.Rating = movie.Rating;
                q.Director = movie.Director;
                q.DirectorId = movie.DirectorId;
                q.Title = movie.Title;
                context.SaveChanges();
            }

        }
        public void delete(Movie movie)
        {
            context.Movies.Remove(movie);
            context.SaveChanges();
        }
        public void create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
        }

        public void Load(string path = "movies.json")  
        {
            //json fájból beolvassa a 100 filmet, hogy tudjatok min lekérdezni
            context.ChangeTracker.LazyLoadingEnabled = false;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var loadedMovies = JsonSerializer.Deserialize<List<Movie>>(File.ReadAllText(path), options);

            foreach (var movie in loadedMovies) //ez azért kell, mert az ID nem érdekes, majd a nav property megoldja
            {
                movie.Id = 0;
                movie.DirectorId = 0;
                movie.Director.Id = 0;

                foreach (var role in movie.Roles)
                {
                    role.Id = 0;
                    role.MovieId = 0;
                    role.ActorId = 0;
                    role.Actor.Id = 0;
                }
            }

            context.Movies.AddRange(loadedMovies);

            context.SaveChanges();
            context.ChangeTracker.LazyLoadingEnabled = true;

        }
    

        public void Save(string path)
        {
            var movies = context.Movies
                .Include(m => m.Director)
                .Include(m => m.Roles).ThenInclude(r => r.Actor)
                .AsNoTracking()
                .ToList();

            File.WriteAllText(path, JsonSerializer.Serialize(movies, options));
        }
    }
}
