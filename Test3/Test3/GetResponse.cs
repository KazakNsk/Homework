using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test3.Models;

namespace Test3
{
    public class GetResponse
    {
        public int totalhits;
        public int len;
        public int offset;
        public IEnumerable<Page> search;
        public GetResponse(IEnumerable<Page> search, int offset, int len)
        {
            totalhits = search.Count();
            this.search = search.Skip(offset).Take(len);
            this.offset = offset;
            this.len = this.search.Count();
        }
        public GetResponse(IEnumerable<Page> search)
        {
            totalhits = search.Count();
            len = totalhits;
            this.search = search;
        }
    }
}
