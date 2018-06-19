using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.DataTransfer
{
    public class PersonDto : DtoBase
    {
        public PersonDto() { }

        //[JsonConstructor]
        public PersonDto(
            string name,
            string vorname,
            DateTime? geburtsdatum,
            Guid id,
            string createdBy,
            DateTime created,
            string modifiedBy,
            DateTime? modified) : base(id, createdBy, created, modifiedBy, modified)
        {
            Name = name;
            Vorname = vorname;
            Geburtsdatum = geburtsdatum;
        }

        public PersonDto(
            string name,
            string vorname,
            DateTime? geburtsdatum,
            string createdBy,
            DateTime created) : base(createdBy, created)
        {
            Name = name;
            Vorname = vorname;
            Geburtsdatum = geburtsdatum;
        }

        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Geburtsdatum { get; set; }
        public ICollection<AdresseDto> Adressen { get; set; }
    }
}
