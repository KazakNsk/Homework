using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test3.Services
{
    public interface IPageService<T> where T : class
    {
        Task<string> Create(T item);
        Task<GetResponse> GetAll();
        Task<GetResponse> Get(string srsearch, int offset, int len);
        Task<string> Update(T item);
        Task<string> Delete(int id);
    }
}
