using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Monolithic.Data;
using Auctions.Monolithic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auctions.Monolithic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutbidsSyncController : ControllerBase
    {
        private readonly AuctionsDbContext _auctionsDbContext;
        public OutbidsSyncController(AuctionsDbContext auctionsDbContext)
        {
            _auctionsDbContext = auctionsDbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Outbid outbid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if amount is not already existed 
                    if (_auctionsDbContext.Outbids.Any(o => o.BidId == outbid.BidId && o.Amount >= outbid.Amount))
                    {
                        //return StatusCode(400, $"someone already did outbid on this amount:{outbid.Amount} or more!");
                    }

                    outbid.Id = Guid.NewGuid();
                    _auctionsDbContext.Outbids.Add(outbid);
                    _auctionsDbContext.SaveChanges();

                    return Ok();
                }
                else
                    return StatusCode(400, ModelState);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}