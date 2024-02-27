namespace AngularHotel.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservedRoom>()
                .HasKey(rr => new { rr.ReservationId, rr.RoomId });


            modelBuilder.Entity<Currency>().HasData(
                    new Currency { Id = 1, Name = "BAM", Price = 1 }, 
                    new Currency { Id = 2, Name = "USD", Price = 1.81m },
                    new Currency { Id = 3, Name = "EUR", Price = 1.95m },
                    new Currency { Id = 4, Name = "GBP", Price = 2.28m } 
                );


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservedRoom> ReservedRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
