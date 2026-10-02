namespace HSZF
{
    using System.Text;
    using System.Text.Json;
    using Data;
    using Data.Entities;
    using HSZF.Data.Repository;

    internal class Program
    {
        static void Main(string[] args)
        {
            MovieManager movieManager = new MovieManager(new MovieRepository());
            movieManager.ReadFile("Data\\Saves\\Movies.json");

            Console.WriteLine(string.Join("\n", movieManager.GetMovies()));





        }
    }
}
