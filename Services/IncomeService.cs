using ExpenseTracker.Components.Pages;
using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class IncomeService
    {
        private readonly ExpenseTrackerContext _context;

        public IncomeService(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<Income>> GetIncomeAsync()
        {
            return await _context.Incomes.ToListAsync();
        }

        public async Task AddIncomeAsync(Income income)
        {
                _context.Incomes.Add(income);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteIncomeAsync(Income income)
        {
                _context.Incomes.Remove(income);
                await _context.SaveChangesAsync();
           
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            _context.Incomes.Update(income);
            await _context.SaveChangesAsync();
            var existingIncome = await _context.Expenses.FindAsync(income.Id);
            if (existingIncome != null)
            {
                existingIncome.Title = income.Title;
                existingIncome.Date =   income.Date;
                existingIncome.Amount = income.Amount;
               
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Expense with ID {income.Id} not found.");
            }
        }

        public async Task<Income> GetIncomeByIdAsync(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public async Task<float> GetTotalIncome(DateTime startdate, DateTime endDate)
        {
            return await _context.Incomes.Where(i => i.Date >= startdate && i.Date <= endDate)
                .SumAsync(i => i.Amount);
        }
    }
}
