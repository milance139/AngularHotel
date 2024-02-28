using AngularHotel.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularHotel.Shared.Models.RequestModels.Reservation
{
    public class CreateReservationRequestModel
    {
        public int? Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int ReservationCommitteeId { get; set; }
        public decimal OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public int CurrencyId { get; set; }
        public List<int> ReservedRoomIds { get; set; }
    }
}
