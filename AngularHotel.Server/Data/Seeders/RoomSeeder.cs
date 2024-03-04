using AngularHotel.Shared.Models.Enums;

namespace AngularHotel.Server.Data.Seeders
{
    public static class RoomSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Room 1", Code = "R1", Capacity = 4, Description = "Description of Room 1", RoomType = RoomType.Apartment },
                new Room { Id = 2, Name = "Room 2", Code = "R2", Capacity = 6, Description = "Description of Room 2", RoomType = RoomType.Sweet },
                new Room { Id = 3, Name = "Room 3", Code = "R3", Capacity = 2, Description = "Description of Room 3", RoomType = RoomType.Duplex },
                new Room { Id = 4, Name = "Room 4", Code = "R4", Capacity = 3, Description = "Description of Room 4", RoomType = RoomType.Apartment },
                new Room { Id = 5, Name = "Room 5", Code = "R5", Capacity = 5, Description = "Description of Room 5", RoomType = RoomType.Sweet },
                new Room { Id = 6, Name = "Room 6", Code = "R6", Capacity = 4, Description = "Description of Room 6", RoomType = RoomType.Duplex }
            );
        }
    }
}
