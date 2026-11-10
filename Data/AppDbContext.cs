using Microsoft.EntityFrameworkCore;
using KanbanBoard.Models;

namespace KanbanBoard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TaskCard> TaskCards { get; set; }
    }
}