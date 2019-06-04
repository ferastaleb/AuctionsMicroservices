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
    public class OutbidsController : ControllerBase
    {
        private readonly AuctionsDbContext _auctionsDbContext;
        public OutbidsController(AuctionsDbContext auctionsDbContext)
        {
            _auctionsDbContext = auctionsDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Outbid outbid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if amount is not already existed 
                    if (await _auctionsDbContext.Outbids.AnyAsync(o => o.BidId == outbid.BidId && o.Amount >= outbid.Amount))
                    {
                        //return StatusCode(400, $"someone already did outbid on this amount:{outbid.Amount} or more!");
                    }

                    outbid.Id = Guid.NewGuid();
                    await _auctionsDbContext.Outbids.AddAsync(outbid);
                    await _auctionsDbContext.SaveChangesAsync();

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