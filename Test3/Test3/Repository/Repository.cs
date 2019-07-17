using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test3.Models;

namespace Test3
{
    public class Repository : IRepository<Page>
    {
        private PageContext _ctx;
        public Repository(PageContext context)
        {
            _ctx = context;
        }

        public async Task<string> Create(Page page)
        { 
            await _ctx.Pages.AddAsync(page);
            await _ctx.SaveChangesAsync();
            return "Page create successfully";
        }

        public async Task<string> Delete(int id)
        {
            if (id < 0)
            {
                throw new Exception("Invalid id");
            }
            Page page = await _ctx.Pages.FindAsync(id);
            if (page != null)
            {
                _ctx.Pages.Remove(page);
                await _ctx.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No such Page with id [" + id + "]");
            }
            return "Page deleted successfully";
        }

        public async Task<IEnumerable<Page>> GetPages(string search)
        {
            return await _ctx.Pages.Where(p => p.title.ToLower().Contains(search.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Page>> GetAll()
        {
            return await _ctx.Pages.ToListAsync();
        }

        public async Task<string> Update(Page item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            if (item.id < 0)
            {
                throw new ArgumentException("Invalid id");
            }
            Page page = await _ctx.Pages.FindAsync(item);
            page.title = item.title;
            page.snippet = item.snippet;
            page.timestamp = item.timestamp;
            await _ctx.SaveChangesAsync();
            return "Page update successfully";
        }
    }
}
