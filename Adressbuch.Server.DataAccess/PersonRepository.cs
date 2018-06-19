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
    public class PersonRepository : IPersonRepository
    {
        private AdressbuchDbContext _adressbuchDbContext;

        public PersonRepository()
        {
            _adressbuchDbContext = new AdressbuchDbContext();
        }


        public void AddPerson(PersonDto personDto)
        {
            var person = _adressbuchDbContext.Personen.Add(CopyDtoToDbModel(personDto));
            _adressbuchDbContext.SaveChanges();
        }

        public void DeletePerson(Guid id)
        {
            _adressbuchDbContext.Personen.Remove(_adressbuchDbContext.Personen.SingleOrDefault(p => p.Id == id));
            _adressbuchDbContext.SaveChanges();
        }

        public void UpdatePerson(Guid id, PersonDto personDto)
        {
            Person person = _adressbuchDbContext.Personen.SingleOrDefault(p => p.Id == id);
            if (null != person)
            {
                person.Name = personDto.Name;
                person.Vorname = personDto.Vorname;
                person.Geburtsdatum = personDto.Geburtsdatum;
                person.Id = personDto.Id;
                person.Created = personDto.Created;
                person.CreatedBy = person.CreatedBy;
                person.Modified = personDto.Modified;
                person.ModifiedBy = personDto.ModifiedBy;
            }

            _adressbuchDbContext.SaveChanges();
        }

        public IEnumerable<PersonDto> GetAll()
        {
            return _adressbuchDbContext.Personen.Select(CopyDbModelToDto).ToList();
        }

        public IEnumerable<PersonDto> GetByAdresseId(Guid adresseId)
        {
            return _adressbuchDbContext.Adressen.SingleOrDefault(a => a.Id == adresseId)?.Personen.Select(CopyDbModelToDto);
        }

        public PersonDto GetById(Guid id)
        {
            var person = _adressbuchDbContext.Personen.SingleOrDefault(p => p.Id == id);

            return CopyDbModelToDto(_adressbuchDbContext.Personen.SingleOrDefault(p => p.Id == id));
        }

        public IEnumerable<PersonDto> Get(PersonDto searchCriteria)
        {
            var filter = PredicateBuilder.True<Person>();

            if (!string.IsNullOrWhiteSpace(searchCriteria.Name))
            {
                if (searchCriteria.Name.StartsWith("%") && searchCriteria.Name.EndsWith("%"))
                {
                    filter = filter.And(p => p.Name.Contains(searchCriteria.Name.Trim(new char[] { '%' })));
                }
                else if (searchCriteria.Name.StartsWith("%"))
                {
                    filter = filter.And(p => p.Name.EndsWith(searchCriteria.Name.Trim(new char[] { '%' })));
                }
                else if (searchCriteria.Name.EndsWith("%"))
                {
                    filter = filter.And(p => p.Name.StartsWith(searchCriteria.Name.Trim(new char[] { '%' })));
                }
                else
                {
                    filter = filter.And(p => 0 == string.Compare(p.Name, searchCriteria.Name.Trim(new char[] { '%' }), true));
                }
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Vorname))
            {
                if (searchCriteria.Vorname.StartsWith("%") && searchCriteria.Vorname.EndsWith("%"))
                {
                    filter = filter.And(p => p.Vorname.Contains(searchCriteria.Vorname.Trim(new char[] { '%' })));
                }
                else if (searchCriteria.Vorname.StartsWith("%"))
                {
                    filter = filter.And(p => p.Vorname.EndsWith(searchCriteria.Vorname.Trim(new char[] { '%' })));
                }
                else if (searchCriteria.Vorname.EndsWith("%"))
                {
                    filter = filter.And(p => p.Vorname.StartsWith(searchCriteria.Vorname.Trim(new char[] { '%' })));
                }
                else
                {
                    filter = filter.And(p => 0 == string.Compare(p.Vorname, searchCriteria.Vorname.Trim(new char[] { '%' }), true));
                }
            }

            return _adressbuchDbContext.Personen.Where(filter).Select(CopyDbModelToDto);
        }

        private PersonDto CopyDbModelToDto(Person person)
        {
            PersonDto personDto = new PersonDto(
                name: person.Name,
                vorname: person.Vorname,
                geburtsdatum: person.Geburtsdatum,
                id: person.Id,
                createdBy: person.CreatedBy,
                created: person.Created,
                modifiedBy: person.ModifiedBy,
                modified: person.Modified);

            personDto.Adressen= new List<AdresseDto>(person.Adressen.Select(a => new AdresseDto(
                   plz: a.Plz,
                   ort: a.Ort,
                   strasse: a.Strasse,
                   hausnr: a.Hausnr,
                   id: a.Id,
                   created: a.Created,
                   createdBy: a.CreatedBy,
                   modified: a.Modified,
                   modifiedBy: a.ModifiedBy)));

            return personDto;
        }

        private Person CopyDtoToDbModel(PersonDto personDto)
        {
            Person person = new Person()
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

            if (null != personDto.Adressen)
            {
                foreach (AdresseDto adresseDto in personDto.Adressen)
                {
                    Adresse adresse = _adressbuchDbContext.Adressen.SingleOrDefault(a => a.Id == adresseDto.Id);

                    if (null == adresse)
                    {
                        adresse = new Adresse()
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
                    }

                    person.Adressen.Add(adresse);
                }
            }
            return person;
        }
    }
}
