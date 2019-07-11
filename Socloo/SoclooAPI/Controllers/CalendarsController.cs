using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : BaseController
    {

        public CalendarsController(IConfiguration config, ILogger logger, DataContext context) :
            base(config, logger, context)
        { }

       
    
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Calendar>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Calendar>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Calendar calendar)
        {
            try
            {
                await UnitOfWork.Repository<Calendar>().InsertAsync(calendar);

                return new OkObjectResult(calendar.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
            
        }


        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Calendar calendar)
        {

            try
            {
                var document = new BsonDocument
            {
                   { "UserId", ObjectId.Parse(calendar.UserId)},
                 { "OccurrencesId", new BsonArray(calendar.OccurrencesId)},
            };

                UnitOfWork.Repository<Calendar>().UpdateAsync(document, ObjectId.Parse(id), "calendars");
                return new OkObjectResult(calendar.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Calendar calendar = (Calendar)this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
            {
                   { "UserId", ObjectId.Parse(calendar.UserId)},
                 { "OccurrencesId", new BsonArray(calendar.OccurrencesId)},
                 { "Deleted", true}
            };
                UnitOfWork.Repository<Assignment>().DeleteAsync(document, ObjectId.Parse(id), "calendars", true);
                return new OkObjectResult(calendar);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}