namespace AngularHotel.Server.Data.Seeders
{
    public static class CurrencySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Name = "BAM", Price = 1 },
                new Currency { Id = 2, Name = "USD", Price = 1.81m },
                new Currency { Id = 3, Name = "EUR", Price = 1.95m },
                new Currency { Id = 4, Name = "GBP", Price = 2.28m }
            );
        }
    }
}
