using ExpenseTracker.Data.Models;
using System;
using System.ComponentModel.DataAnnotations; 
public class Expense
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public float Amount { get; set; }

    public bool Planned { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
