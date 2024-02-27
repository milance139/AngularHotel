using AngularHotel.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularHotel.Shared.Models.Entities
{
    public class Room : DomainObject
    {

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public int Capacity { get; set; } 

        public string Description { get; set; } = string.Empty;
        public RoomType RoomType { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
