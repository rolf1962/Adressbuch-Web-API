using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Adressbuch.DataTransfer;
using Adressbuch.Server.DbModel;
using Adressbuch.Tools;

namespace Adressbuch.Server.DataAccess
{
    public class AdresseRepository : IAdresseRepository
    {
        private AdressbuchDbContext _adressbuchDbContext;

        public AdresseRepository()
        {
            _adressbuchDbContext = new AdressbuchDbContext();
        }


        public void AddAdresse(AdresseDto AdresseDto)
        {
            var Adresse = _adressbuchDbContext.Adressen.Add(CopyDtoToDbModel(AdresseDto));
            _adressbuchDbContext.SaveChanges();
        }

        public void DeleteAdresse(Guid id)
        {
            _adressbuchDbContext.Adressen.Remove(_adressbuchDbContext.Adressen.SingleOrDefault(p => p.Id == id));
            _adressbuchDbContext.SaveChanges();
        }

        public void UpdateAdresse(Guid id, AdresseDto adresseDto)
        {
            Adresse Adresse = _adressbuchDbContext.Adressen.SingleOrDefault(a => a.Id == id);
            if (null != Adresse)
            {
                Adresse.Plz = adresseDto.Plz;
                Adresse.Ort = adresseDto.Ort;
                Adresse.Strasse = adresseDto.Strasse;
                Adresse.Hausnr = adresseDto.Hausnr;
                Adresse.Id = adresseDto.Id;
                Adresse.Created = adresseDto.Created;
                Adresse.CreatedBy = Adresse.CreatedBy;
                Adresse.Modified = adresseDto.Modified;
                Adresse.ModifiedBy = adresseDto.ModifiedBy;
            }
        }

        public IEnumerable<AdresseDto> GetAll()
        {
            return _adressbuchDbContext.Adressen.Select(CopyDbModelToDto).ToList();
        }

        public IEnumerable<AdresseDto> GetByPersonId(Guid personId)
        {
            return _adressbuchDbContext.Personen.SingleOrDefault(a => a.Id == personId)?.Adressen.Select(CopyDbModelToDto);
        }

        public AdresseDto GetById(Guid id)
        {
            return CopyDbModelToDto(_adressbuchDbContext.Adressen.SingleOrDefault(p => p.Id == id));
        }

        public IEnumerable<AdresseDto> Get(AdresseDto searchCriteria)
        {
            var filter = PredicateBuilder.True<Adresse>();

            if (!string.IsNullOrWhiteSpace(searchCriteria.Plz))
            {
                if (searchCriteria.Plz.StartsWith("%") && searchCriteria.Plz.EndsWith("%"))
                {
                    filter = filter.And(p => p.Plz.Contains(searchCriteria.Plz.Replace("%", "")));
                }
                else if (searchCriteria.Plz.StartsWith("%"))
                {
                    filter = filter.And(p => p.Plz.EndsWith(searchCriteria.Plz.Replace("%", "")));
                }
                else if (searchCriteria.Plz.EndsWith("%"))
                {
                    filter = filter.And(p => p.Plz.StartsWith(searchCriteria.Plz.Replace("%", "")));
                }
                else
                {
                    filter = filter.And(p => 0 == string.Compare(p.Plz, searchCriteria.Plz.Replace("%", ""), true));
                }
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Ort))
            {
                if (searchCriteria.Ort.StartsWith("%") && searchCriteria.Ort.EndsWith("%"))
                {
                    filter = filter.And(p => p.Ort.Contains(searchCriteria.Ort.Replace("%", "")));
                }
                else if (searchCriteria.Ort.StartsWith("%"))
                {
                    filter = filter.And(p => p.Ort.EndsWith(searchCriteria.Ort.Replace("%", "")));
                }
                else if (searchCriteria.Ort.EndsWith("%"))
                {
                    filter = filter.And(p => p.Ort.StartsWith(searchCriteria.Ort.Replace("%", "")));
                }
                else
                {
                    filter = filter.And(p => 0 == string.Compare(p.Ort, searchCriteria.Ort.Replace("%", ""), true));
                }
            }

            return _adressbuchDbContext.Adressen.Where(filter).Select(CopyDbModelToDto);
        }

        private AdresseDto CopyDbModelToDto(Adresse Adresse)
        {
            AdresseDto adresseDto = new AdresseDto(
                plz: Adresse.Plz,
                ort: Adresse.Ort,
                strasse: Adresse.Strasse,
                hausnr: Adresse.Hausnr,
                id: Adresse.Id,
                createdBy: Adresse.CreatedBy,
                created: Adresse.Created,
                modifiedBy: Adresse.ModifiedBy,
                modified: Adresse.Modified);

            adresseDto.Personen = new List<PersonDto>(Adresse.Personen.Select(p => new PersonDto(
                    name: p.Name,
                    vorname: p.Vorname,
                    geburtsdatum: p.Geburtsdatum,
                    id: p.Id,
                    created: p.Created,
                    createdBy: p.CreatedBy,
                    modified: p.Modified,
                    modifiedBy: p.ModifiedBy)));

            return adresseDto;
        }

        private Adresse CopyDtoToDbModel(AdresseDto adresseDto)
        {
            Adresse Adresse = new Adresse()
            {
                Plz = adresseDto.Plz,
                Ort = adresseDto.Ort,
                Strasse = adresseDto.Strasse,
                Hausnr = adresseDto.Hausnr,
                Id = adresseDto.Id,
                Created = adresseDto.Created,
                CreatedBy = adresseDto.CreatedBy,
                Modified = adresseDto.Modified,
                ModifiedBy = adresseDto.ModifiedBy
            };

            foreach (PersonDto personDto in adresseDto.Personen)
            {
                Person person = _adressbuchDbContext.Personen.SingleOrDefault(a => a.Id == adresseDto.Id);

                if (null == person)
                {
                    person = new Person()
                    {
                        Name = personDto.Name,
                        Vorname = personDto.Vorname,
                        Geburtsdatum = personDto.Geburtsdatum,
                        Id = personDto.Id,
                        Created = personDto.Created,
                        CreatedBy = personDto.CreatedBy,
                        Modified = personDto.Modified,
                        ModifiedBy = personDto.ModifiedBy
                    };
                }

                Adresse.Personen.Add(person);
            }

            return Adresse;
        }
    }
}
