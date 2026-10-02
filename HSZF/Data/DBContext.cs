using HSZF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSZF.Data
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public MovieContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //adatbázis hiba esetén csináljatok egy új mdf állományt, majd kérjétek meg, hogy mindig másolja az .exe mellé (copy to output directory = copy always)
                var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Movies.mdf");
                var con = $@"Server=(localdb)\MSSQLLocalDB;AttachDbFilename={dbPath};Database=Movies;Integrated Security=True;MultipleActiveResultSets=true";
                optionsBuilder.UseSqlServer(con);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>(m => m.HasOne<Director>().WithMany().HasForeignKey(m => m.DirectorId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Movie>().HasOne(m=>m.Director).WithMany().HasForeignKey(m => m.DirectorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>().HasOne(r => r.Actor).WithMany(a => a.Roles).HasForeignKey(r => r.ActorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>().HasOne(r => r.Movie).WithMany(m => m.Roles).HasForeignKey(r => r.MovieId).OnDelete(DeleteBehavior.Cascade);



        }

    }
}
