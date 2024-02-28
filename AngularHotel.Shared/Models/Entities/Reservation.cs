using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AngularHotel.Shared.Models.Entities
{
    public class Reservation : DomainObject
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int ReservationCommitteeId { get; set; }
        public User ReservationCommittee { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        public int Discount { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public bool IsCancelled { get; set; } = false;
        [JsonIgnore]
        public List<ReservedRoom> ReservedRooms { get; set; }
    }
}
