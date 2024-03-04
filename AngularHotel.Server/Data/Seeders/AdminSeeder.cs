using System.Security.Cryptography;
using System.Text;

namespace AngularHotel.Server.Data.Seeders
{
    public static class AdminSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var adminEmail = "admin@live.com";
            var adminPassword = "Test_123";

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(adminPassword, out passwordHash, out passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = adminEmail,
                    Name = "Admin",
                    LastName = "User",
                    PhoneNumber = "123456789",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    DateCreated = DateTime.Now,
                    Role = "Admin"
                }
            );
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
