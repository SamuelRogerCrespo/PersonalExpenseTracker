//TestDB implementation before submitting
using Microsoft.EntityFrameworkCore;

namespace PersonalExpenseTracker
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ExpenseTracker.db");
        }
    }
}
