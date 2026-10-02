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
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonInclude]
        public int Id { get; set; }

        [JsonInclude]
        public int Priority { get; set; }

        [JsonInclude]
        public string RoleName { get; set; }

        [JsonInclude]
        public int MovieId { get; set; }

        [JsonInclude]
        public int ActorId { get; set; }

        [JsonInclude]
        public virtual Actor Actor { get; set; }

        [JsonInclude]
        public virtual Movie Movie { get; set; }

        [JsonConstructor]
        public Role()
        {
            
        }
    }
}
