using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVillageCinemas.ViewModels
{
    public class PaymentViewModel
    {
        public Movie? Movie { get; set; }
        public AvailableDate? AvailableDate { get; set; }

        public List<AvailableSeat>? AvailableSeats { get; set; }

        public List<string>? AvailableSeatsCodes { get; set; }

        public Order? Order { get; set; }

        public Ticket? Ticket { get; set; }

        public List<Ticket>? TicketList { get; set; }

        public void InitMovie()
        {
            Movie = Ticket?.AvailableSeat.AvailableDate.Movie;
        }    

        public void InitAvailableDate()
        {
            AvailableDate = Ticket?.AvailableSeat.AvailableDate;
        }

        public void InitAvailableSeats()
        {
            AvailableSeats = TicketList?.Select(ticket => ticket.AvailableSeat).ToList();
        }

        public void InitAvailableSeatsCodes()
        {
            AvailableSeatsCodes = AvailableSeats?.Select(availableSeat => availableSeat.Code).ToList();
        }

        public void InitOrder(IOrderService orderService, int orderId)
        {
            Order = orderService.GetSingleOrder(orderId);
        }

        public void InitTicket()
        {
            Ticket = TicketList?.First();
        }

        public void InitTicketList(ITicketService ticketService, int orderId)
        {
            TicketList = ticketService.GetAllTickets(orderId);
        }

        public PaymentViewModel(IOrderService orderService, ITicketService ticketService, int orderId)
        {
            InitOrder(orderService, orderId);
            InitTicketList(ticketService, orderId);
            InitTicket();
            InitMovie();
            InitAvailableDate();
            InitAvailableSeats();
            InitAvailableSeatsCodes();
        }
    }
}
