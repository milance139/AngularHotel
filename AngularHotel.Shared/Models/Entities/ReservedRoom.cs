using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularHotel.Shared.Models.Entities
{
    public class ReservedRoom : DomainObject
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
