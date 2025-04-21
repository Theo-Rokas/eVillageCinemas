using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.ViewModels;

namespace eVillageCinemas.Services.ImplementCode
{
    public class SeatService : ISeatService
    {
        private readonly DatabaseContext _eVCDB;

        public SeatService(DatabaseContext eVDB)
        {
            _eVCDB = eVDB;
        }        

        public void CreateSeat(AdminViewModel adminViewModel)
        {
            for (int i = 0; i < adminViewModel.MultipleNumber; i++)
            {
                var seat = new Seat()
                {
                    Code = adminViewModel.Seat?.Code?.Trim(),
                    HallId = adminViewModel.Hall.HallId
                };

                _eVCDB.Seats.Add(seat);
            }
            _eVCDB.SaveChanges();
        }        

        public void UpdateSeat(AdminViewModel adminViewModel)
        {
            var seat = GetSingleSeat(adminViewModel.Seat.SeatId);
            seat.Code = adminViewModel.Seat?.Code?.Trim();
            seat.HallId = adminViewModel.Hall.HallId;
            _eVCDB.SaveChanges();
        }

        public void DeleteAllSeats()
        {
            var seats = GetAllSeats();
            _eVCDB.RemoveRange(seats);
            _eVCDB.SaveChanges();
        }

        public void DeleteSingleSeat(int seatId)
        {
            var seat = GetSingleSeat(seatId);
            _eVCDB.Remove(seat);
            _eVCDB.SaveChanges();
        }

        public Seat GetSingleSeat(int seatId)
        {
            return _eVCDB.Seats.Single(seat => seat.SeatId == seatId);
        }

        public List<Seat> GetAllSeats(int hallId = 0)
        {
            if (hallId == 0)
                return _eVCDB.Seats.ToList();
            else
                return _eVCDB.Seats.Where(seat => seat.HallId == hallId).ToList();
        }

        public dynamic GetAllSeatsDatatable(int draw, int start, int length, Dictionary<string, string> search)
        {
            var seats = GetAllSeats();
            var recordsTotal = seats.Count;

            var searchValue = search["value"];
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                seats = seats.Where(seat => seat.Code.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            seats = seats.Skip(start).Take(length).ToList();

            var data = new List<object>();
            foreach (var seat in seats)
            {
                data.Add(new object[]
                {
                    seat.SeatId,
                    seat.Code ?? "",
                    seat.Hall.Name ?? "",
                    "<button type='button' class='btn btn-primary' onclick='CreateOrUpdateSeatGet(" + seat.SeatId + ")'>Update Seat</button>",
                    "<button type='button' class='btn btn-danger' onclick='DeleteSingleOrAllSeats(" + seat.SeatId + ")'>Delete Seat</button>"
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
