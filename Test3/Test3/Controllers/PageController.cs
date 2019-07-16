using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private PageValidator pageValidator;
        public PageController(PageContext ctx)
        {
            IRepository<Page> db = new Repository(ctx);
            service = new PageService(db);
            pageValidator = new PageValidator();
        }
       

        [EnableCors("Policy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(Page page)
        {
            try
            {
                pageValidator.ValidateAndThrow(page);
                return Ok(await service.Create(page));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [EnableCors("Policy")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(Page page)
        {
            try
            {
                pageValidator.ValidateAndThrow(page);
                return Ok(await service.Update(page));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [EnableCors("Policy")]
        [HttpDelete("{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int value)
        {
            try
            {
                if (value < 0)
                {
                    throw new ValidationException("Invalid id");
                }
                return Ok(await service.Delete(value));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }     
        
    }
}
   