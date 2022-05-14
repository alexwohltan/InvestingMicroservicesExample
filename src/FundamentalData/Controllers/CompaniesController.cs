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
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : CRUDController<Company>
    {
        public CompaniesController(FundamentalDataContext context)
            : base(context, context.Companies)
        {

        }

        // POST api/Companies
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult> Post([FromBody] Company company)
        {
            if (_table.Where(e => e.Name == company.Name).Count() > 0)
                return BadRequest(String.Format("Company {0} already exists.", company.Name));

            await _context.AddAsync(company);
            await _context.SaveChangesAsync();

            var result = _table.Where(e => e.Name == company.Name).FirstOrDefault();

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            NewValueEvent(result);

            return CreatedAtAction(nameof(GetByID), new { id = result.ID }, result);
        }
    }
}
