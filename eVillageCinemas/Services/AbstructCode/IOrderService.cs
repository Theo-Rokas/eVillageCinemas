using eVillageCinemas.Models;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.Abstruct
{
    public interface IOrderService
    {
        public IFormFile CreateQRCode(int orderId);

        public string CheckInOrder(int orderId);

        public int CreateOrder(OrderViewModel clientViewModel);

        public int UpdateOrder(OrderViewModel clientViewModel);

        public void DeleteSingleOrder(int orderId);

        public void DeleteAllOrders();

        public Order GetSingleOrder(int orderId);

        public List<Order> GetAllOrders();

        public dynamic GetAllOrdersDatatable(int draw, int start, int length, Dictionary<string, string> search);
    }
}
