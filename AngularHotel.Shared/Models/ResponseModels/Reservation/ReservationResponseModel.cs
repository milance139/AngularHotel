using AngularHotel.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularHotel.Shared.Models.ResponseModels.Reservation
{
    public class ReservationResponseModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string ReservationCommitteeFullName { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public Currency Currency { get; set; }
        public bool IsCancelled { get; set; }
        public List<string> ReservedRooms { get; set; }
    }
}
