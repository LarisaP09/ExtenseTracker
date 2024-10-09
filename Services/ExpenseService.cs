using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            return await _context.Expenses
                                 .Include(e => e.Category)
                                 .FirstOrDefaultAsync(e => e.Id == expenseId);
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
            var existingExpense = await _context.Expenses.FindAsync(expense.Id);
            if (existingExpense != null)
            {
                existingExpense.Title = expense.Title;
                existingExpense.Date = expense.Date;
                existingExpense.Amount = expense.Amount;
                existingExpense.Planned = expense.Planned;
                existingExpense.CategoryId = expense.CategoryId;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Expense with ID {expense.Id} not found.");
            }
        }

        public async Task<List<Expense>> GetExpensesByCategoryAsync(int categoryId)
        {
            return await _context.Expenses
                                 .Where(e => e.CategoryId == categoryId)
                                 .Include(e => e.Category)
                                 .ToListAsync();
        }

        public async Task<float> GetTotalExpanse(DateTime startdate, DateTime endDate)
        {
            return await _context.Expenses.Where(e => e.Date >= startdate && e.Date <= endDate)
                .SumAsync(e => e.Amount);
        }
    }
}
