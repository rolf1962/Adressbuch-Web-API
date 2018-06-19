using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DbModel
{
    [Table("Adresse")]
    public class Adresse : EntityBase
    {
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Strasse { get; set; }
        public string Hausnr { get; set; }
        public virtual ICollection<Person> Personen { get; } = new HashSet<Person>();
    }
}
