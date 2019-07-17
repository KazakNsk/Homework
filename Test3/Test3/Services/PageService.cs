using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test3.GlobalErrorHandling.Extensions;
using Test3.Models;

namespace Test3.Services
{
    public class PageService : IPageService<Page>
    {
        private IRepository<Page> db;

        public PageService(IRepository<Page> repository)
        {
            db = repository;

        }

        public async Task<string> Create(Page item)
        {
            IEnumerable<Page> pages = await db.GetAll();
            if (pages.Any(p => p.title == item.title))
            {
                throw new ValidationException("Title must be unique");
            }
            item.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return await db.Create(item);
        }

        public async Task<string> Update(Page item)
        {
            IEnumerable<Page> pages = await db.GetAll();
            if (pages.Any(p => p.title == item.title && p.id != item.id))
            {
                throw new ValidationException("Title must be unique");
            }
            item.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return await db.Update(item);
        }

        public async Task<string> Delete(int id)
        {
            IEnumerable<Page> pages = await db.GetAll();
            if (pages.Any(p => p.id != id))
            {
                throw new ValidationException("Indalid id");
            }
            return await db.Delete(id);
        }

        public async Task<GetResponse> GetAll()
        {
            IEnumerable<Page> pages = await db.GetAll();
            return new GetResponse(pages);
        }
        public async Task<GetResponse> Get(string srsearch, int offset, int len)
        {
            IEnumerable<Page> pages = await db.GetPages(srsearch);
            return new GetResponse(pages, offset, len);
        }
    }
}
