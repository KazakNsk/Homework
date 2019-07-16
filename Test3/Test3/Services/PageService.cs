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
        private PageValidator pageValidator;
        public PageService(IRepository<Page> repository)
        {
            db = repository;
            pageValidator = new PageValidator();
        }

        public async Task<string> Create(Page item)
        {
            pageValidator.ValidateAndThrow(item);
            item.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return await db.Create(item);
        }

        public async Task<string> Update(Page item)
        {
            pageValidator.ValidateAndThrow(item);
            item.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return await db.Update(item);
        }

        public async Task<string> Delete(int id)
        {
            if (id < 0)
            {
                throw new ValidationException("Invalid id");
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
            if (string.IsNullOrEmpty(srsearch))
            {
                throw new ArgumentException("'srsearch' argument is null or empty");
            }
            IEnumerable<Page> pages = await db.GetPages(srsearch);
            return new GetResponse(pages, offset, len);
        }
    }
}
