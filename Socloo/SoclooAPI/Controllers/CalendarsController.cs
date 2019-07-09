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

        public CalendarsController(IConfiguration config, ILogger<CalendarsController> logger, DataContext context) :
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
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Calendar> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Calendar>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Calendar calendar)
        {

            await UnitOfWork.Repository<Calendar>().InsertAsync(calendar);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string _id, [FromBody] Calendar calendar)
        {

            try
            {
                var document = new BsonDocument
            {
                   { "UserId", ObjectId.Parse(calendar.UserId)},
                 { "OccurrencesId", new BsonArray(calendar.OccurrencesId)},
            };

                UnitOfWork.Repository<Calendar>().Update(document, ObjectId.Parse(_id), "calendars");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] Calendar calendar)
        {
            try
            {
                var document = new BsonDocument
            {
                   { "UserId", ObjectId.Parse(calendar.UserId)},
                 { "OccurrencesId", new BsonArray(calendar.OccurrencesId)},
            };
                UnitOfWork.Repository<Assignment>().Delete(document, ObjectId.Parse(id), "assignments", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}