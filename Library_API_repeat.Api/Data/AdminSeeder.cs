using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Data
{
    public class AdminSeeder
    {
        public static async Task SeedAdminAsync(
           IServiceProvider services,
           IConfiguration configuration)
        {
            using var scope = services.CreateScope();

            var context =
                scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

            var adminEmail = configuration["Admin:Email"];

            if (string.IsNullOrWhiteSpace(adminEmail))
            {
                return;
            }

            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == adminEmail);

            if (user == null)
            {
                return;
            }

            if (user.Role != "Admin")
            {
                user.Role = "Admin";

                await context.SaveChangesAsync();
            }
        }
    }
}
