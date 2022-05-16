using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundamentalData
{
    [Route("api/Industries")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        private readonly FundamentalDataContext _repository;
        private readonly IEventBus _eventBus;

        public IndustriesController(FundamentalDataContext context, IEventBus eventBus)
        {
            _repository = context;
            _eventBus = eventBus;
        }


        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetIndustryById")]
        public virtual async Task<ActionResult<Industry>> GetByID(int id)
        {
            return (await _repository.GetIndustry(id)).WithoutFilings();
        }
        // GET api/<controller>/names/
        [HttpGet("names/", Name = "GetIndustryByNameAndSectorId")]
        public virtual async Task<ActionResult<Industry>> GetByName(string name, int sectorId)
        {
            return (await _repository.GetIndustry(sectorId, name)).WithoutFilings();
        }

        // GET: api/<controller>/5/Companies
        [HttpGet("{id}/Companies", Name = "GetCompaniesByIndustryId")]
        public async virtual Task<IEnumerable<Company>> Get(int industryId)
        {
            return (await _repository.GetCompanies(industryId)).Select(e => e.WithoutFilings());
        }

        // POST api/<controller>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] Industry newIndustry, int sectorId)
        {
            var result = await _repository.AddIndustry(newIndustry, sectorId);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Post), new { id = result.ID }, result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Industry newIndustryValues)
        {
            var result = await _repository.UpdateIndustry(id, newIndustryValues);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return AcceptedAtAction(nameof(Put), new { id = result.ID }, result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteIndustry(id);

            return Ok();
        }
    }
}
