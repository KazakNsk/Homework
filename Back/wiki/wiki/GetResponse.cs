using System.Collections.Generic;
using System.Linq;
using wiki.Models;

namespace wiki
{
    public class GetResponse
    {
        public int totalhits;
        public int len;
        public int offset;
        public IEnumerable<Page> search;
        public GetResponse(IEnumerable<Page> search, int offset, int len)
        {
            this.totalhits = search.Count();
            this.search = search.Skip(offset).Take(len);
            this.offset = offset;
            this.len = this.search.Count();
        }
        public GetResponse(IEnumerable<Page> search)
        {
            this.totalhits = search.Count();
            this.len = totalhits;
            this.search = search;
        }
    }
}
