using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DbModel
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }
    }
}
