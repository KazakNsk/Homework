using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test3
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetPages(string search);       
        Task<string> Create(T item);
        Task<string> Update(T item);
        Task<string> Delete(int id);
    }
}
