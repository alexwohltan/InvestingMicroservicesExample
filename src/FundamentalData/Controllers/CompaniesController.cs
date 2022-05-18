using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataStructures;
using System;
using System.Collections.Generic;
namespace FundamentalData
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly FundamentalDataContext _repository;
        private readonly IEventBus _eventBus;

        public CompaniesController(FundamentalDataContext context, IEventBus eventBus)
        {
            _repository = context;
            _eventBus = eventBus;
        }


        // GET api/<controller>/5
        [HttpGet("ids", Name = "GetCompanyById")]
        public virtual async Task<ActionResult<Company>> GetByID(int id)
        {
            return await _repository.GetCompany(id);
        }
        // GET api/<controller>/names/
        [HttpGet("tickers", Name = "GetCompanyByTicker")]
        public virtual async Task<ActionResult<Company>> GetByTicker(string ticker)
        {
            return await _repository.GetCompany(ticker);
        }

        // GET: api/<controller>/5/Filings
        [HttpGet("{id}/Filings", Name = "GetFilingsByCompanyId")]
        public async virtual Task<IEnumerable<Filing>> Get(int companyId)
        {
            return await _repository.GetFilings(companyId);
        }

        // POST api/<controller>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] Company newCompany, int industryId)
        {
            try
            {
                var result = await _repository.AddCompany(newCompany, industryId);

                if (result == null)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return CreatedAtAction(nameof(Post), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        // POST api/<controller>
        [HttpPost("names/")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] Company newCompany, string marketName, string sectorName, string industryName)
        {
            try
            {
                if (sectorName == null)
                    sectorName = "";
                if (industryName == null)
                    industryName = "";

                var result = await _repository.AddCompany(newCompany, industryName, sectorName, marketName);

                if (result == null)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                return CreatedAtAction(nameof(Post), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Company newCompanyValues)
        {
            var result = await _repository.UpdateCompany(id, newCompanyValues);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return AcceptedAtAction(nameof(Put), new { id = result.ID }, result);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteCompany(id);

            return Ok();
        }
    }
}
