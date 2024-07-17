using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
	public class ExpenseTrackerContext :DbContext
	{
		public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options) 
			: base(options) 
		{
		}

		public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
		public DbSet<Income> Incomes { get; set; }

        public override int SaveChanges()
        {
            ConvertDatesToUtc();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertDatesToUtc();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDatesToUtc()
        {
            var entriesExpense = ChangeTracker.Entries()
                .Where(e => e.Entity is Expense && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entriesExpense)
            {
                var expense = (Expense)entityEntry.Entity;
                expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Utc);
            }

            var entriesIncome = ChangeTracker.Entries()
                .Where(e => e.Entity is Income && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entriesIncome)
            {
                var income = (Income)entityEntry.Entity;
                income.Date = DateTime.SpecifyKind(income.Date, DateTimeKind.Utc);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

            modelBuilder.Entity<Category>()
				.HasData(new Category[]
				{
					new Category { Id = 1,Name = "Food"},
					new Category { Id = 2,Name = "Travel"},
					new Category { Id = 3,Name = "Entertainment"},
					new Category { Id = 4,Name = "Education"},
					new Category { Id = 5,Name = "Clothes"},
					new Category { Id = 6,Name = "House"},
				});

           
        }
	}
}
