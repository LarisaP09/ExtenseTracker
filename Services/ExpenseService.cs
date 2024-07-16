using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Components.Pages;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly ExpenseTrackerContext _context;

        public ExpenseService(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _context.Expenses
                                 .Include(e => e.Category)
                                 .ToListAsync();
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            var existExpense = await _context.Expenses.FindAsync(expense.Id);
            if (existExpense != null) 
            {
                existExpense.Title = expense.Title;
                existExpense.Date = expense.Date;
                existExpense.Amount = expense.Amount;
                existExpense.Planned = expense.Planned;
                existExpense.CategoryId = expense.CategoryId;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Expense with ID {expense.Id} not found.");
            }
        }
    }
}
