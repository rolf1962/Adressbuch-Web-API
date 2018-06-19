using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DbModel
{
    [Table("Person")]
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Geburtsdatum { get; set; }
        public virtual ICollection<Adresse> Adressen { get; } = new HashSet<Adresse>();
    }
}
