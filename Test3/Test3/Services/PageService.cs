using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            item.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return await db.Create(item);
        }

        public async Task<string> Update(Page item)
        {
           // IEnumerable<Page> pages = await db.GetAll();        
            item.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return await db.Update(item);
        }

        public async Task<string> Delete(int id)
        {
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
