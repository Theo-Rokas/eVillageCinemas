using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace eVillageCinemas.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? QRCode { get; set; }

        public bool CheckIn { get; set; }

        public int? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
