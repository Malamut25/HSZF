using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace HSZF.Data.Entities
{
    public class Movie
    {
        //biztosan legyen majd benne a fájlban
        [JsonInclude]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonInclude]
        [StringLength(240)]
        public string Title { get; set; }

        [JsonInclude]
        public DateTime? ReleaseDate { get; set; }

        [JsonInclude]
        public int DirectorId { get; set; }

        [JsonInclude]
        public virtual Director Director { get; set; }

        [JsonInclude]
        [Range(0, 10)]
        public double Rating { get; set; }

        [JsonInclude]
        public virtual ICollection<Actor> Actors { get; set; }
        [JsonInclude]
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        
        public Movie(int id, string title, DateTime? releaseDate, int directorId, double rating)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            DirectorId = directorId;
            Rating = rating;
        }

        //ezt a konstruktort használja a visszaalakításnál
        [JsonConstructor]
        public Movie()
        {
            
        }


        public override string ToString()
        {
            return $"{Title}\t{Rating}\t{ReleaseDate?.Date.ToString("yyyy.MM.dd")}";
        }
    }
}
