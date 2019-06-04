using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auctions.Monolithic.Models
{
    public class Outbid
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BidId { get; set; }

        [Required]
        public float Amount { get; set; }
    }
}
