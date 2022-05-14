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
    public class IndustriesController : CRUDController<Industry>
    {
        public IndustriesController(FundamentalDataContext context)
            : base(context, context.Industries)
        {

        }

        // POST api/Industries
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult> Post([FromBody] Industry industry)
        {
            if (_table.Where(e => e.Name == industry.Name).Count() > 0)
                return BadRequest();

            await _context.AddAsync(industry);
            await _context.SaveChangesAsync();

            var result = _table.Where(e => e.Name == industry.Name).FirstOrDefault();

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetByID), new { id = result.ID }, result);
        }
    }
}
