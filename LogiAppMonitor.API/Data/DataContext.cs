using LogiAppMonitor.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LogiAppMonitor.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<LogMessage> LogMessages { get; set; }
    }
}