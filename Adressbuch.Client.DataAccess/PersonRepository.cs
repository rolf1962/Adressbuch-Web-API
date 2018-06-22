using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Adressbuch.Client.ViewModel;
using Adressbuch.DataTransfer;
using Newtonsoft.Json;

namespace Adressbuch.Client.DataAccess
{
    public class PersonRepository
    {
        private const string _uri = "http://localhost:61352";
        private HttpClient _httpClient;

        public PersonRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_uri);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private PersonViewModel CopyDtoToViewModel(PersonDto dto)
        {
            return new PersonViewModel()
            {
                Id = dto.Id,
                Created = dto.Created,
                CreatedBy = dto.CreatedBy,
                Modified = dto.Modified,
                ModifiedBy = dto.ModifiedBy,
                Name = dto.Name,
                Vorname = dto.Vorname,
                Geburtsdatum = dto.Geburtsdatum
            };
        }

        private PersonDto CopyViewModelToDto(PersonViewModel viewModel)
        {
            PersonDto personDto = null;
            if (Guid.Empty == viewModel.Id)
            {
                personDto = new PersonDto(
                    name: viewModel.Name,
                    vorname: viewModel.Vorname,
                    geburtsdatum: viewModel.Geburtsdatum,
                    createdBy: viewModel.CreatedBy,
                    created: viewModel.Created);

                viewModel.Id = personDto.Id;
            }
            else
            {
                personDto = new PersonDto(
                    id: viewModel.Id,
                    name: viewModel.Name,
                    vorname: viewModel.Vorname,
                    geburtsdatum: viewModel.Geburtsdatum,
                    createdBy: viewModel.CreatedBy,
                    created: viewModel.Created,
                    modifiedBy: viewModel.ModifiedBy,
                    modified: viewModel.Modified);
            }

            return personDto;
        }

        public async Task Delete(Guid id)
        {
            string requestUri = string.Format("/api/people/{0}", id);

            using (HttpResponseMessage serverResponse = await _httpClient.DeleteAsync(requestUri))
            {
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetAll()
        {
            IEnumerable<PersonViewModel> returnValue = null;
            string requestUri = "/api/people";

            using (HttpResponseMessage response = await _httpClient.GetAsync(requestUri))
            {
                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var personDtos = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(responseBody);
                    returnValue = personDtos.Select(CopyDtoToViewModel);
                }
            }

            return returnValue;
        }

        public async Task<PersonViewModel> GetById(Guid id)
        {
            PersonViewModel returnValue = null;
            string requestUri = string.Format("/api/people/{0}", id);

            using (HttpResponseMessage serverResponse = await _httpClient.GetAsync(requestUri))
            {
                using (HttpContent content = serverResponse.Content)
                {
                    string responseBody = await serverResponse.Content.ReadAsStringAsync();
                    var personDto = JsonConvert.DeserializeObject<PersonDto>(responseBody);
                    returnValue = CopyDtoToViewModel(personDto);
                }
            }

            return returnValue;
        }

        public async Task Insert(PersonViewModel viewModel)
        {
            string requestUri = "/api/people";
            string requestBody = JsonConvert.SerializeObject(CopyViewModelToDto(viewModel));

            using (var serverResponse = await _httpClient.PostAsync(requestUri, new StringContent(requestBody, Encoding.UTF8, "application/json")))
            {
            }
        }

        public async Task Update(Guid id, PersonViewModel viewModel)
        {
            string requestUri = string.Format("/api/people/{0}", viewModel.Id);

            viewModel.Modified = DateTime.Now;
            viewModel.ModifiedBy = "Auch Rolf";

            string requestBody = JsonConvert.SerializeObject(CopyViewModelToDto(viewModel));

            using (var serverResponse = await _httpClient.PutAsync(requestUri, new StringContent(requestBody, Encoding.UTF8, "application/json")))
            {
            }
        }

        private PersonSearchDto CopySearchViewModelToDto(PersonSearchViewModel viewModel)
        {
            PersonSearchDto personSearchDto = new PersonSearchDto();

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                personSearchDto.Name.Value = null;
                personSearchDto.Name.LogicalOperator = Common.LogicalOperators.Equals;
                personSearchDto.Name.IsSpecified = false;
            }
            else
            {
                personSearchDto.Name.Value = viewModel.Name;
                personSearchDto.Name.LogicalOperator = viewModel.NameLO;
                personSearchDto.Name.IsSpecified = true;
            }

            if (string.IsNullOrWhiteSpace( viewModel.Vorname))
            {
                personSearchDto.Vorname.Value = null;
                personSearchDto.Vorname.LogicalOperator = Common.LogicalOperators.Equals;
                personSearchDto.Vorname.IsSpecified = false;
            }
            else
            {
                personSearchDto.Vorname.Value = viewModel.Vorname;
                personSearchDto.Vorname.LogicalOperator = viewModel.VornameLO;
                personSearchDto.Vorname.IsSpecified = true;
            }

            if (null == viewModel.GeburtsdatumVon)
            {
                personSearchDto.GeburtsdatumVon.Value = null;
                personSearchDto.GeburtsdatumVon.LogicalOperator = Common.LogicalOperators.Equals;
                personSearchDto.GeburtsdatumVon.IsSpecified = false;
            }
            else
            {
                personSearchDto.GeburtsdatumVon.Value = viewModel.GeburtsdatumVon;
                personSearchDto.GeburtsdatumVon.LogicalOperator = viewModel.GeburtsdatumVonLO;
                personSearchDto.GeburtsdatumVon.IsSpecified = true;
            }

            if (null == viewModel.GeburtsdatumBis)
            {
                personSearchDto.GeburtsdatumBis.Value = null;
                personSearchDto.GeburtsdatumBis.LogicalOperator = Common.LogicalOperators.Equals;
                personSearchDto.GeburtsdatumBis.IsSpecified = false;
            }
            else
            {
                personSearchDto.GeburtsdatumBis.Value = viewModel.GeburtsdatumBis;
                personSearchDto.GeburtsdatumBis.LogicalOperator = viewModel.GeburtsdatumBisLO;
                personSearchDto.GeburtsdatumBis.IsSpecified = true;
            }

            return personSearchDto;
        }

        public async Task<IEnumerable<PersonViewModel>> GetFiltered(PersonSearchViewModel viewModel)
        {
            IEnumerable<PersonViewModel> returnValue = null;
            PersonSearchDto personSearchDto = CopySearchViewModelToDto(viewModel);

            string requestUri = string.Format($"/api/people/search");

            var requestBody = JsonConvert.SerializeObject(personSearchDto);
            var stringContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(requestUri, stringContent, CancellationToken.None))
            {
                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var personDtos = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(responseBody);
                    returnValue = personDtos.Select(CopyDtoToViewModel);
                }
            }

            return returnValue;
        }
    }
}
