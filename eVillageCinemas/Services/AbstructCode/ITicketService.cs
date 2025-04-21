using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface ITicketService
    {
        public void ReleaseSeats(PaymentViewModel clientViewModel);

        public void CreateTicket(OrderViewModel clientViewModel);

        public void UpdateTicket(OrderViewModel clientViewModel);

        public void DeleteSingleTicket(int ticketId);

        public void DeleteAllTickets();

        public Ticket GetSingleTicket(int ticketId);

        public List<Ticket> GetAllTickets(int orderId = 0);

        public dynamic GetAllTicketsDatatable(int draw, int start, int length, Dictionary<string, string> search);        
    }
}
