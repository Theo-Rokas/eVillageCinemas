using eVillageCinemas.Models;
using eVillageCinemas.Services.AbstructCode;
using System.Security.Cryptography;
using System.Text;

namespace eVillageCinemas.Services.ImplementCode
{
    public class PaymentService : IPaymentService
    {
        private readonly DatabaseContext _eVCDB;
        private readonly string SharedSecret = "Cardlink1";

        public PaymentService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }

        public string CalculateDigest(Payment payment)
        {
            string concatenatedString = $"{payment.Version}{payment.Mid}" +
                                        $"{payment.OrderId}{payment.OrderDesc}{payment.OrderAmount}{payment.Currency}" +
                                        $"{payment.PayerEmail}{payment.BillCountry}{payment.BillZip}" +
                                        $"{payment.BillCity}{payment.BillAddress}{payment.ConfirmUrl}{payment.CancelUrl}" +
                                        $"{SharedSecret}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(concatenatedString);
                byte[] hashBytes = sha256.ComputeHash(utf8Bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public Payment CreatePayment(Order order)
        {
            var payment = new Payment
            {
                Version = "2",
                Mid = "9000004855",
                OrderId = order.OrderId,
                OrderDesc = "",
                OrderAmount = order.Tickets.Sum(ticket => ticket.AvailableSeat.AvailableDate.Movie.Price),
                Currency = "EUR",
                PayerEmail = order.Email,
                BillCountry = "GR",
                BillZip = "",
                BillCity = "",
                BillAddress = "",
                ConfirmUrl = "https://192.168.1.201:7028/Payment/Confirm?orderId=" + order.OrderId,
                CancelUrl = "https://192.168.1.201:7028/Payment/Cancel?orderId=" + order.OrderId
            };

            string digest = CalculateDigest(payment);

            payment.Digest = digest;

            _eVCDB.Payments.Add(payment);
            _eVCDB.SaveChanges();

            order.PaymentId = payment.PaymentId;
            _eVCDB.SaveChanges();

            return payment;
        }
    }
}
