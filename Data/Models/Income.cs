using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Data.Models
{
    public class Income
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;


        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public float Amount { get; set; }

        public IncomeType? Type { get; set; } = null;
    }

    public enum IncomeType
    {
        Salary,
        Scholarship,
        Gift,
        LuckyWinnings
    }
}
