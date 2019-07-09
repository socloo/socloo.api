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
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Dashboard> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Dashboard>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Dashboard dashboard)
        {

            await UnitOfWork.Repository<Dashboard>().InsertAsync(dashboard);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string _id, [FromBody] Dashboard dash)
        {

            
            try
            {
                var document = new BsonDocument
            {
                 { "PostsId", new BsonArray(dash.PostsId)},
            };
                UnitOfWork.Repository<Chat>().Update(document, ObjectId.Parse(_id), "dashboards");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] Dashboard dash)
        {
            try
            {
                var document = new BsonDocument
            {
                 { "PostsId", new BsonArray(dash.PostsId)},
            };
                UnitOfWork.Repository<Dashboard>().Delete(document, ObjectId.Parse(id), "dashboards", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}