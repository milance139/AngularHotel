namespace AngularHotel.Server.Data.Seeders
{
    public static class ReservationSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, From = new DateTime(2024, 3, 01), To = new DateTime(2024, 3, 02), ReservationCommitteeId = 1, OriginalPrice = 100, Discount = 10, TotalPrice = 90, CurrencyId = 1, IsCancelled = false },
                new Reservation { Id = 2, From = new DateTime(2024, 3, 03), To = new DateTime(2024, 3, 04), ReservationCommitteeId = 1, OriginalPrice = 150, Discount = 20, TotalPrice = 120, CurrencyId = 2, IsCancelled = false },
                new Reservation { Id = 3, From = new DateTime(2024, 3, 15), To = new DateTime(2024, 3, 20), ReservationCommitteeId = 1, OriginalPrice = 200, Discount = 0, TotalPrice = 200, CurrencyId = 1, IsCancelled = false },
                new Reservation { Id = 4, From = new DateTime(2024, 3, 18), To = new DateTime(2024, 3, 25), ReservationCommitteeId = 1, OriginalPrice = 100, Discount = 0, TotalPrice = 100, CurrencyId = 2, IsCancelled = false },
                new Reservation { Id = 5, From = new DateTime(2024, 3, 20), To = new DateTime(2024, 3, 28), ReservationCommitteeId = 1, OriginalPrice = 180, Discount = 10, TotalPrice = 162, CurrencyId = 1, IsCancelled = false }
            };
            modelBuilder.Entity<Reservation>().HasData(reservations);
           
            var reservedRooms = new List<ReservedRoom>
            {
                new ReservedRoom { Id = 1, ReservationId = 1, RoomId = 1 },
                new ReservedRoom { Id = 2, ReservationId = 2, RoomId = 2 },
                new ReservedRoom { Id = 3, ReservationId = 3, RoomId = 3 },
                new ReservedRoom { Id = 4, ReservationId = 4, RoomId = 4 },
                new ReservedRoom { Id = 5, ReservationId = 5, RoomId = 5 }
            };

            modelBuilder.Entity<ReservedRoom>().HasData(reservedRooms);
        }
    }
}
