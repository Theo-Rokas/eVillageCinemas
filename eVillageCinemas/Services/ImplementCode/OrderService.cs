using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.Services.AbstructCode;
using eVillageCinemas.ViewModels;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Windows.Compatibility;

namespace eVillageCinemas.Services.ImplementCode
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _eVCDB;

        private readonly IHelperService _helperService;

        public OrderService(DatabaseContext eVDB, IHelperService helperService)
        {
            _eVCDB = eVDB;
            _helperService = helperService;
        }

        public IFormFile CreateQRCode(int orderId)
        {
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 200,
                    Height = 200
                }
            };

            var barcodeBitmap = barcodeWriter.Write(orderId.ToString());

            var stream = new MemoryStream();
            barcodeBitmap.Save(stream, ImageFormat.Png);
            stream.Position = 0;

            var formFile = new FormFile(stream, 0, stream.Length, "QRCode", $"QRCode_{orderId}.png");
            return formFile;
        }

        public string CheckInOrder(int orderId)
        {
            var message = "";
            var order = GetSingleOrder(orderId);

            if (order.CheckIn)
            {
                message = "This order has already been checked in.";
            }
            else
            {
                order.CheckIn = true;
                _eVCDB.SaveChanges();

                message = "Order successfully checked in.";
            }

            return message;
        }


        public int CreateOrder(OrderViewModel clientViewModel)
        {
            var order = new Order()
            {
                FirstName = clientViewModel.Order?.FirstName?.Trim(),
                LastName = clientViewModel.Order?.LastName?.Trim(),
                Email = clientViewModel.Order?.Email?.Trim(),
            };            

            _eVCDB.Orders.Add(order);
            _eVCDB.SaveChanges();

            var formFile = CreateQRCode(order.OrderId);
            var qrCodeUrl = _helperService.UploadFileAsync(formFile).Result;

            order.QRCode = qrCodeUrl;
            order.CheckIn = false;

            return order.OrderId;
        }        

        public int UpdateOrder(OrderViewModel clientViewModel)
        {
            var order = GetSingleOrder(clientViewModel.Order.OrderId);
            order.FirstName = clientViewModel.Order?.FirstName?.Trim();
            order.LastName = clientViewModel.Order?.LastName?.Trim();
            order.Email = clientViewModel.Order?.Email?.Trim();
            _eVCDB.SaveChanges();

            return order.OrderId;
        }

        public void DeleteAllOrders()
        {
            var orders = GetAllOrders();
            _eVCDB.RemoveRange(orders);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleOrder(int orderId)
        {
            var order = GetSingleOrder(orderId);
            _eVCDB.Remove(order);
            _eVCDB.SaveChanges();
        }

        public Order GetSingleOrder(int orderId)
        {
            return _eVCDB.Orders.Single(order => order.OrderId == orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _eVCDB.Orders.ToList();
        }

        public dynamic GetAllOrdersDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var orders = GetAllOrders();
            var recordsTotal = orders.Count;

            var searchValue = search["value"];
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                orders = orders.Where(order => 
                    order.FirstName.ToLower().Contains(searchValue.ToLower()) ||
                    order.LastName.ToLower().Contains(searchValue.ToLower()) ||
                    order.Email.ToLower().Contains(searchValue.ToLower())
                ).ToList();
            }

            orders = orders.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var order in orders)
            {
                data.Add(new object[]
                {
                    order.OrderId,
                    order.FirstName ?? "",
                    order.LastName ?? "",
                    order.Email ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateOrderGet(" + order.OrderId + ")'>Update Seat</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllOrders(" + order.OrderId + ")'>Delete Seat</button>"
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
