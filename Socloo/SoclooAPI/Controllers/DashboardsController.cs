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
    public class DashboardsController : BaseController
    {

        public DashboardsController(IConfiguration config, ILogger<DashboardsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Dashboard>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<Dashboard>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dashboard dash)
        {
            try
            {
                await UnitOfWork.Repository<Dashboard>().InsertAsync(dash);
                return new OkObjectResult(dash.Id);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }

        }


        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Dashboard dash)
        {

            
            try
            {
                var document = new BsonDocument
            {
                 { "PostsId", new BsonArray(dash.PostsId)},
            };
                UnitOfWork.Repository<Chat>().UpdateAsync(document, ObjectId.Parse(id), "dashboards");
                return new OkObjectResult(dash.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Dashboard dash = (Dashboard)this.GetById(id).Result;
            try
            {
                var document = new BsonDocument
            {
                 { "PostsId", new BsonArray(dash.PostsId)},
                 { "Deleted", true}
            };
                UnitOfWork.Repository<Dashboard>().DeleteAsync(document, ObjectId.Parse(id), "dashboards", true);
                return new OkObjectResult(dash);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }

    }
}