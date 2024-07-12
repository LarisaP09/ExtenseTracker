using ExpenseTracker.Data.Models;
namespace ExpenseTracker.Data.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public float Amount { get; set; }
        public bool Planned { get; set; }

        // Foreign key for Category
        public int CategoryId { get; set; }
        
        // Navigation property
        public Category Category { get; set; }

    }
}
