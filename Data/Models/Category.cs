namespace ExpenseTracker.Data.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// Collection of related Expenses
		public ICollection<Expense> Expenses { get; set; }
    }
}
