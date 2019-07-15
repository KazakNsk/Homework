using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wiki.Models;

namespace wiki
{
    public class SQLPageRepository : IRepository<Page>
    {
        private AppDatabaseContext _ctx;

        public SQLPageRepository(AppDatabaseContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<string> Create(Page item)
        {
            await _ctx.Pages.AddAsync(item);
            return "Page create successfully";
        }

        public async Task<string> Delete(int id)
        {
            if(id<0)
            {
                throw new Exception("Invalid id");
            }
            Page page = await _ctx.Pages.FindAsync(id);
            if ( page != null)
            {
                _ctx.Pages.Remove(page);
                await _ctx.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No such Page with id ["+id+"]");
            }
            return "Page deleted successfully";
        }

        public async Task<IEnumerable<Page>> GetPages(string srsearch)
        {
            return await _ctx.Pages.Where(p => p.title.ToLower().Contains(srsearch.ToLower())).ToListAsync();
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
