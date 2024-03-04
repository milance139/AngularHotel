using AngularHotel.Server.Data.Seeders;

namespace AngularHotel.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservedRoom>()
                .HasKey(rr => new { rr.Id, rr.ReservationId, rr.RoomId });

            AdminSeeder.Seed(modelBuilder);
            CurrencySeeder.Seed(modelBuilder);
            RoomSeeder.Seed(modelBuilder);
            ReservationSeeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservedRoom> ReservedRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
