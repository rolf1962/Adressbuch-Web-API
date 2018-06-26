using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Adressbuch.Common;
using Adressbuch.DataTransfer;
using Adressbuch.Server.DataAccess;
using Adressbuch.Server.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace Adressbuch.Server.DataService.Controllers
{
    /// <summary>
    /// ASP.Net Controller to handle requests for <see cref="PersonDto"/>-Objects
    /// </summary>
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPersonRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">The repository for receiving and sending <see cref="PersonDto"/>-Data</param>
        public PeopleController(IPersonRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Default Request, returns a list with all avaible <see cref="PersonDto"/>-Objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        /// <summary>
        /// Request for a single <see cref="PersonDto"/>-Object
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the requested <see cref="PersonDto"/>-Object</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }

        /// <summary>
        /// Request for <see cref="PersonDto"/>-Objects, with the submitted searchcriterias
        /// </summary>
        /// <param name="searchCriteria">A <see cref="PersonSearchDto"/>-Object with searchcriterias.</param>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<IActionResult> PostAndGetBySearchCriteria([FromBody]PersonSearchDto searchCriteria)
        {
            return Ok(await _repository.GetByCriteriaAsync(searchCriteria));
        }

        /// <summary>
        /// Request for <see cref="PersonDto"/>-Objects, with the submitted searchcriterias
        /// </summary>
        /// <param name="searchCriteria">A <see cref="PersonSearchDto"/>-Object with searchcriterias.</param>
        /// <returns></returns>
        /// <ToDo>TypeError: HEAD or GET Request cannot have a body. -> Add <see cref="PersonSearchDto"/> to Header.</ToDo>
        //[HttpGet("search")]
        //public async Task<IActionResult> GetBySearchCriteria([FromBody]PersonSearchDto searchCriteria)
        //{
        //    return Ok(await _repository.GetByCriteriaAsync(searchCriteria));
        //}

        /// <summary>
        /// Delete-Request for removing a specified <see cref="PersonDto"/>-Object (from the database)
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the <see cref="PersonDto"/> to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeletePersonAsync(id);
            return Ok();
        }
    }
}