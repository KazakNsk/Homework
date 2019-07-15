using System.Collections.Generic;
using System.Threading.Tasks;

namespace wiki
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetPages(string srsearch);
        Task<IEnumerable<T>> GetAll();
        Task<string> Create(T item);
        Task<string> Update(T item);
        Task<string> Delete(int id);
    }
}
