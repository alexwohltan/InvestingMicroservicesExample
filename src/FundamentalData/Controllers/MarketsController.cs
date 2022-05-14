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
    [Route("api/Markets")]
    [ApiController]
    public class MarketsController : CRUDController<Market>
    {
        public MarketsController(FundamentalDataContext context)
            : base(context, context.Markets)
        {

        }
    }
}