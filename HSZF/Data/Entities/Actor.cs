using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HSZF.Data.Entities
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonInclude]
        public int Id { get; set; }
        [Required]
        [StringLength(240)]
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public virtual ICollection<Movie> Movies { get; set; }

        [JsonInclude]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
