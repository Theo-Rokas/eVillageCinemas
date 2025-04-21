using eVillageCinemas.Models;

namespace eVillageCinemas.Services.AbstructCode
{
    public interface IPaymentService
    {
        public Payment CreatePayment(Order order);

        public string CalculateDigest(Payment payment);
    }
}
