using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.Services.AbstructCode;
using eVillageCinemas.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eVillageCinemas.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ITicketService _ticketService;
        private readonly IPaymentService _paymentService;
        private readonly IHelperService _helperService;

        private static string _orderTicket =
        "<div style=\"font-family: Arial, sans-serif; color: #000; margin: 0 auto; padding: 16px; max-width: 800px;\">" +
        "    <header style=\"text-align: center; margin-bottom: 24px;\">" +
        "        <h1 style=\"font-size: 2em; font-weight: bold; margin: 0;\">eVillage Cinemas</h1>" +
        "    </header>" +
        "    <div style=\"background-color: #d4edda; color: #155724; padding: 16px; border: 1px solid #c3e6cb; text-align: center; border-radius: 4px; margin-bottom: 24px;\">" +
        "        <strong>Payment successful!</strong> Your order has been placed." +
        "    </div>" +
        "    <div style=\"display: flex; flex-wrap: wrap; justify-content: center; margin: 0; border: 1px solid #ccc; border-radius: 8px; overflow: hidden;\">" +
        "        <div style=\"flex: 2; padding: 16px; background-color: #f8f9fa; border-right: 1px dashed #000;\">" +
        "            <h5 style=\"font-size: 1.25em; font-weight: bold; text-transform: uppercase; margin-bottom: 16px;\">Order Details</h5>" +
        "            <p><strong>First Name:</strong> {{First_Name}}</p>" +
        "            <p><strong>Last Name:</strong> {{Last_Name}}</p>" +
        "            <p><strong>Movie Title:</strong> {{Movie_Title}}</p>" +
        "            <p><strong>Movie Date:</strong> {{Movie_Date}}</p>" +
        "            <p><strong>Seats:</strong> {{Movie_Seats}}</p>" +
        "        </div>" +
        "        <div style=\"flex: 1; padding: 16px; background-color: #f8f9fa; text-align: center;\">" +
        "            <h5 style=\"font-size: 1.25em; font-weight: bold; text-transform: uppercase; margin-bottom: 16px;\">QR Code</h5>" +
        "            <div style=\"margin-top: 8px;\">" +
        "                <img src=\"{{QR_Code}}\" alt=\"QR Code\" style=\"max-width: 100%; border-radius: 4px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);\">" +
        "            </div>" +
        "        </div>" +
        "    </div>" +
        "</div>";

        public PaymentController(
            IOrderService orderService,
            ITicketService ticketService,
            IPaymentService paymentService,
            IHelperService helperService
        )
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _paymentService = paymentService;
            _helperService = helperService;
        }

        public IActionResult Create(int orderId)        
        {
            var order = _orderService.GetSingleOrder(orderId);
            var payment = _paymentService.CreatePayment(order);
            return View(payment);
        }

        public IActionResult Confirm(int orderId)
        {
            PaymentViewModel clientViewModel = new PaymentViewModel(_orderService, _ticketService, orderId);

            _orderTicket = _orderTicket
            .Replace("{{First_Name}}", clientViewModel.Order?.FirstName)
            .Replace("{{Last_Name}}", clientViewModel.Order?.LastName) 
            .Replace("{{Movie_Title}}", clientViewModel.Movie?.Title)
            .Replace("{{Movie_Date}}", clientViewModel.AvailableDate?.Date.ToString("dd/MM/yyyy HH:mm"))
            .Replace("{{Movie_Seats}}", string.Join(",", clientViewModel.AvailableSeatsCodes))
            .Replace("{{QR_Code}}", clientViewModel.Order?.QRCode);

            _helperService.SendEmailAsync("t.rokas@hotmail.com", clientViewModel.Order.Email, "Registration Confirmation", _orderTicket);

            return View(clientViewModel);
        }

        public IActionResult Cancel(int orderId)
        {
            PaymentViewModel clientViewModel = new PaymentViewModel(_orderService, _ticketService, orderId);
            _ticketService.ReleaseSeats(clientViewModel);
            return View(clientViewModel);
        }
    }
}
