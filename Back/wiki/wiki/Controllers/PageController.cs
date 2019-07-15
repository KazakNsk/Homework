using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using wiki.Models;
using wiki.Services;
using FluentValidation;
using System.Threading.Tasks;

namespace wiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private IPageService<Page> service;
        private PageValidator pageValidator;
        public PageController(AppDatabaseContext ctx)
        {
            IRepository<Page> db = new SQLPageRepository(ctx);
            service = new PageService(db);
            pageValidator = new PageValidator();
        }
        [EnableCors("Policy")]
        [HttpPost("{title,snippet}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(string title, string snippet)
        {
            try
            {
                Page page = new Page
                {
                    title = title,
                    snippet = snippet
                };
                pageValidator.ValidateAndThrow(page);
                return Ok(await service.Create(page));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
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
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        [EnableCors("Policy")]
        [HttpPut("{id,title,snippet}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(int id, string title, string snippet)
        {
            try
            {
                Page page = new Page
                {
                    id = id,
                    title = title,
                    snippet = snippet
                };
                pageValidator.ValidateAndThrow(page);
                return Ok(await service.Update(page));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
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
            catch (Exception e)
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
                if(value<0)
                {
                    throw new ValidationException("Invalid id");
                }
                return Ok(await service.Delete(value));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        [EnableCors("Policy")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            { 
                return Ok(await service.GetAll());
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        [EnableCors("Policy")]
        [HttpGet("{srsearch,offset,len}")]
        public async Task<ActionResult> Get(string srsearch, int offset, int len)
        {
            try
            {
                return Ok(await service.Get(srsearch, offset, len));
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}