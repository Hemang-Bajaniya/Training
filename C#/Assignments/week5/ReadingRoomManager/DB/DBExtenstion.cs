using Microsoft.EntityFrameworkCore;

namespace ReadingRoomManager.DB
{
    public static class DBExtenstion
    {
        // to make sure all migration done on app starting
        public static void MigrateDB(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
