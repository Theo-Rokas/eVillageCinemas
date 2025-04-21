using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVillageCinemas.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public string? Version { get; set; }

        public string? Mid { get; set; }

        public string? OrderDesc { get; set; }

        public decimal? OrderAmount { get; set; }

        public string? Currency { get; set; }

        public string? PayerEmail { get; set; }

        public string? BillCountry { get; set; }

        public string? BillZip { get; set; }

        public string? BillCity { get; set; }

        public string? BillAddress { get; set; }

        public string? ConfirmUrl { get; set; }

        public string? CancelUrl { get; set; }

        public string? Digest { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
