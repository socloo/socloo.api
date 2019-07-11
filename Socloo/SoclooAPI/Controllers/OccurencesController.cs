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
        public OccurencesController(IConfiguration config, ILogger logger, DataContext context) :
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
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Occurrence>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Occurrence occurrence)
        {
            try
            {

            

            await UnitOfWork.Repository<Occurrence>().InsertAsync(occurrence);
            return new OkObjectResult(occurrence.Id);

        }catch(Exception ex){
                return new BadRequestResult();
    }
}


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Occurrence occurrence)
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
                return new OkObjectResult(occurrence.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            try
            {
                Occurrence occurrence = (Occurrence)this.GetById(id).Result;
                var document = new BsonDocument
            {
                 {"Type",occurrence.Type },
                 { "TeacherId", ObjectId.Parse(occurrence.TeacherId)},
                 { "Date",Convert.ToDateTime(occurrence.Date)},
                { "Info", ""+occurrence.Info},
                  {"Deleted",true }
            };
                UnitOfWork.Repository<Occurrence>().DeleteAsync(document, ObjectId.Parse(id), "occurrences", true);
                return new OkObjectResult(occurrence);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}