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
    public class OccurencesController : BaseController
    {
        public OccurencesController(IConfiguration config, ILogger<OccurencesController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Occurrence>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Occurrence> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Occurrence>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Occurrence occurrence)
        {

            await UnitOfWork.Repository<Occurrence>().InsertAsync(occurrence);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Occurrence occurrence)
        {

            try
            {
                var document = new BsonDocument
            {
                 {"Type",occurrence.Type },
                 { "TeacherId", ObjectId.Parse(occurrence.TeacherId)},
                 { "Date",Convert.ToDateTime(occurrence.Date)},
                { "Info", ""+occurrence.Info},
            };

                UnitOfWork.Repository<Occurrence>().UpdateAsync(document, ObjectId.Parse(id), "occurrences");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id)
        {
            try
            {
                Occurrence occurrence = this.GetById(id).Result;
                var document = new BsonDocument
            {
                 {"Type",occurrence.Type },
                 { "TeacherId", ObjectId.Parse(occurrence.TeacherId)},
                 { "Date",Convert.ToDateTime(occurrence.Date)},
                { "Info", ""+occurrence.Info},
                  {"Deleted",true }
            };
                UnitOfWork.Repository<Occurrence>().DeleteAsync(document, ObjectId.Parse(id), "occurrences", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}