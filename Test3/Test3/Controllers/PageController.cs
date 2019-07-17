using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test3.Models;
using Test3.Services;

namespace Test3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private IPageService<Page> service;    
        public PageController(PageContext ctx)
        {
            IRepository<Page> db = new Repository(ctx);
            service = new PageService(db);       
        }

        [EnableCors("Policy")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
             return Ok(await service.GetAll());
        }


        [EnableCors("Policy")]
        [HttpGet("{srsearch,offset,len}")]
        public async Task<ActionResult> Get(string srsearch, int offset, int len)
        {

            return Ok(await service.Get(srsearch, offset, len));
        }


        [EnableCors("Policy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(Page page)
        {

            return Ok(await service.Create(page));

        }
        [EnableCors("Policy")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(Page page)
        {
                return Ok(await service.Update(page));
        }


        [EnableCors("Policy")]
        [HttpDelete("{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int value)
        {
               return Ok(await service.Delete(value));
        }
   }     
        
}

   