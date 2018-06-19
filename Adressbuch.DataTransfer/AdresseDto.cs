using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.DataTransfer
{
    public class AdresseDto : DtoBase
    {
        public AdresseDto() { }

        //[JsonConstructor]
        public AdresseDto(
            string plz, 
            string ort, 
            string strasse, 
            string hausnr, 
            Guid id,
            string createdBy,
            DateTime created,
            string modifiedBy,
            DateTime? modified) : base(id, createdBy, created, modifiedBy, modified)
        {
            Plz = plz;
            Ort = ort;
            Strasse = strasse;
            Hausnr = hausnr;
        }

        public AdresseDto(
            string plz, 
            string ort, 
            string strasse, 
            string hausnr, 
            string createdBy,
            DateTime created) : base(createdBy, created)
        {
            Plz = plz;
            Ort = ort;
            Strasse = strasse;
            Hausnr = hausnr;
        }

        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Strasse { get; set; }
        public string Hausnr { get; set; }
        public ICollection<PersonDto> Personen { get; set; }
    }
}
