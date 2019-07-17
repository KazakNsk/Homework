using System;
using System.Linq;
using Test3.Models;

namespace Test3
{
    public static class SampleData
    {
        public static void Initialize(PageContext context)
        {
            if (!context.Pages.Any())
            {
                context.Pages.AddRange(
                    new Page
                    {
                        title = "1TestPage",
                        snippet = "test test test",
                        timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds
            },
                    new Page
                    {
                        title = "2TestPage",
                        snippet = "test2 test2 test2",
                        timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds
                    },
                    new Page
                    {
                        title = "3TestPage",
                        snippet = "test3 test3 test3",
                        timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds
                    }
                ); ;
                context.SaveChanges();
            }
        }
    }
}