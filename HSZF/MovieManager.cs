using HSZF.Data;
using HSZF.Data.Entities;
using HSZF.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HSZF
{
    public class MovieManager
    {
        JsonSerializerOptions options;
        IMovieRepository context;
       
        public MovieManager(IMovieRepository context)
        {
            
            this.context=context;
        }

        public void ReadFile(string path)
        {
            this.context.Load(path);
        }
        public void SaveFile(string path)
        {
            this.context.Save(path);
        }

        public List<Movie> GetMovies()
        {
            return this.context.Movies.ToList();
        }

        //feladat.txt-ben leírt feladatokat itt kellene megoldani


        /*
        1. Melyik filmnek van a legjobb értékelése?
        Figyelem: holtverseny esetén mind kellene.
        */
        public List<Movie> TopMovies() 
        { 
              //return context.Movies.OrderByDescending(m => m.Rating).Take(1).ToList();
             var maxRating= this.context.Movies.Max(x => x.Rating);
             return this.context.Movies.Where(x => x.Rating == maxRating).ToList();

        }

        /*2. Melyik filmek rendezője egy adott rendező?
        Paraméter: string directorName.
        */

        public List<Movie> GetMoviesByDirector(string directorName)
        {
            return this.context.Movies.Where(x => x.Director.Name == directorName).ToList();

        }
        /*3. Filmek listája cím és megjelenési év formában.
        Adj vissza egy gyűjteményt ahol minden elem tartalmazza a Title-t és a kiadás évét. 
        (Tipp: anonim típus vagy tuple.)*/

        public List<Tuple<string, DateTime?>> ListFilms()
        {
            
            
            return this.context.Movies.Select(x => Tuple.Create( x.Title, x.ReleaseDate )).ToList();
           
            
            
          

        }

        /*4. Igaz-e hogy minden film értékelése jobb egy adott értéknél?
        Paraméter: double minRating.*/

        public bool AllMoviesRatingAbove(double minRating)
        {
            return this.context.Movies.All(x => x.Rating > minRating);

        }
        /*5. Melyik a legjobb film egy adott időintervallumban?
        Paraméterek: DateTime from, DateTime to.
        */
        public Movie BestMovieInterval(DateTime from,DateTime to)
        {
            return this.context.Movies.Where(x => x.ReleaseDate >= from && x.ReleaseDate <= to).MaxBy(x=>x.Rating);
        }










    }
}
