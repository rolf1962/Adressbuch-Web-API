using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Adressbuch.DataTransfer;
using Adressbuch.Server.DbModel;
using System.Data.Entity;

namespace Adressbuch.Server.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private AdressbuchDbContext _adressbuchDbContext;

        public PersonRepository(AdressbuchDbContext adressbuchDbContext)
        {
            _adressbuchDbContext = adressbuchDbContext;
        }


        public async Task AddPersonAsync(PersonDto personDto)
        {
            _adressbuchDbContext.Personen.Add(CopyDtoToDbModel(personDto));
            await _adressbuchDbContext.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Guid id)
        {
            _adressbuchDbContext.Personen.Remove(await _adressbuchDbContext.Personen.SingleOrDefaultAsync(p => p.Id == id));
            await _adressbuchDbContext.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Guid id, PersonDto personDto)
        {
            Person person = await _adressbuchDbContext.Personen.SingleOrDefaultAsync(p => p.Id == id);
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

            await _adressbuchDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var dbResult = await _adressbuchDbContext.Personen.ToListAsync();
            return dbResult.Select(CopyDbModelToDto);
        }

        public async Task< IEnumerable<PersonDto>> GetByAdresseIdAsync(Guid adresseId)
        {
            var dbResult = await _adressbuchDbContext.Adressen
                .SingleOrDefaultAsync(a => a.Id == adresseId);
            return dbResult.Personen.Select(CopyDbModelToDto);
        }

        public async Task<PersonDto> GetByIdAsync(Guid id)
        {
            return CopyDbModelToDto(await _adressbuchDbContext.Personen.SingleOrDefaultAsync(p => p.Id == id));
        }

        public async Task< IEnumerable<PersonDto>> GetByCriteriaAsync(PersonDto searchCriteria)
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

            var dbResult = await Task.Run(()=> _adressbuchDbContext.Personen.Where(filter));
            return dbResult.Select(CopyDbModelToDto);
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

            personDto.Adressen = new List<AdresseDto>(person.Adressen.Select(a => new AdresseDto(
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
