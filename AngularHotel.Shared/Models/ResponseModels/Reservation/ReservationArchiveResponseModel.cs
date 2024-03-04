using AngularHotel.Shared.Models.Entities;
using AngularHotel.Shared.Models.ResponseModels.CurrencyResponse;

namespace AngularHotel.Shared.Models.ResponseModels.Reservation
{
    public class ReservationArchiveResponseModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string ReservationCommitteeFullName { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public Currency Currency { get; set; }
        public TotalPriceInBAMAndEurResponseModel TotalPriceInBAMAndEur { get; set; }
        public bool IsCancelled { get; set; }
        public List<string> ReservedRooms { get; set; }
    }
}
