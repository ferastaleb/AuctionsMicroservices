using System;
using System.ComponentModel.DataAnnotations;

namespace Auctions.OutbidsAPI.Models
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
