using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Page>> GetPages(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentException("'srsearch' argument is null or empty");
            }
            return await db.GetPages(search);
        }
    }
}
