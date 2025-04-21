using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class TicketService : ITicketService
    {
        private readonly DatabaseContext _eVCDB;

        public TicketService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }

        public void ReleaseSeats(PaymentViewModel clientViewModel)
        {
            foreach (var availableSeatVM in clientViewModel.AvailableSeats)
            {
                var availableSeatDB = _eVCDB.AvailableSeats.Single(availableSeat => availableSeat.AvailableSeatId == availableSeatVM.AvailableSeatId);
                availableSeatDB.IsAvailable = true;
            }

            _eVCDB.SaveChanges();
        }

        public void CreateTicket(OrderViewModel clientViewModel)
        {
            foreach(var availableSeatVM in clientViewModel.AvailableSeats)
            {
                var availableSeatDB = _eVCDB.AvailableSeats.Single(availableSeat => availableSeat.AvailableSeatId == availableSeatVM.AvailableSeatId);
                availableSeatDB.IsAvailable = false;

                var ticket = new Ticket()
                {
                    OrderId = clientViewModel.Order.OrderId,
                    AvailableSeatId = availableSeatDB.AvailableSeatId
                };

                _eVCDB.Tickets.Add(ticket);
            }
            
            _eVCDB.SaveChanges();
        }        

        public void UpdateTicket(OrderViewModel clientViewModel)
        {
            foreach (var availableSeat in clientViewModel.AvailableSeats)
            {
                var ticket = GetSingleTicket(clientViewModel.Ticket.TicketId);
                ticket.OrderId = clientViewModel.Order.OrderId;
                ticket.AvailableSeatId = availableSeat.AvailableSeatId;
            }

            _eVCDB.SaveChanges();
        }

        public void DeleteAllTickets()
        {
            var tickets = GetAllTickets();
            _eVCDB.RemoveRange(tickets);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleTicket(int ticketId)
        {
            var ticket = GetSingleTicket(ticketId);
            _eVCDB.Remove(ticket);
            _eVCDB.SaveChanges();
        }

        public Ticket GetSingleTicket(int ticketId)
        {
            return _eVCDB.Tickets.Single(ticket => ticket.TicketId == ticketId);
        }

        public List<Ticket> GetAllTickets(int orderId = 0)
        {
            if(orderId == 0)
                return _eVCDB.Tickets.ToList();
            else
                return _eVCDB.Tickets.Where(ticket => ticket.OrderId == orderId).ToList();
        }

        public dynamic GetAllTicketsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var tickets = GetAllTickets();
            var recordsTotal = tickets.Count;            

            tickets = tickets.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var ticket in tickets)
            {
                data.Add(new object[]
                {
                    ticket.TicketId,
                    ticket.Order.Email ?? "",
                    ticket.AvailableSeat.Code ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateTicketGet(" + ticket.TicketId + ")'>Update Ticket</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllTickets(" + ticket.TicketId + ")'>Delete Ticket</button>"
                });
            }

            var json = new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,
                data = data
            };

            return json;
        }               
    }
}
