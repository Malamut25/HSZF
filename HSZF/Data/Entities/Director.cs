using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HSZF.Data.Entities
{
    public class Director
    {
        [JsonInclude]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonInclude]
        [StringLength(240)]
        public string Name { get; set; }

        [JsonInclude]
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

        public Director(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonConstructor]
        public Director()
        {
            
        }
    }
}
