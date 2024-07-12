using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Components.Pages;
using Microsoft.AspNetCore.Http.HttpResults;



namespace ExpenseTracker.Services
{
    public class CategoryService
    {
        private readonly ExpenseTrackerContext _context;

        public CategoryService(ExpenseTrackerContext context)
        {
            _context = context; 
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsyync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
