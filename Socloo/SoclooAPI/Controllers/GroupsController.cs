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
    public class GroupsController : BaseController
    {

        public GroupsController(IConfiguration config, ILogger<GroupsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Group>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<Group>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Group group)
        {
            try
            {
                await UnitOfWork.Repository<Group>().InsertAsync(group);

                return new OkObjectResult(group.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
           
        }

        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Group group)
        {

           
            try
            {
                var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(group.StudentsId)},
                { "TeachersId", new BsonArray(group.TeachersId)},
                { "Name", "" + group.Name},
                { "Info", "" + group.Info},
                { "PictureId", ObjectId.Parse(group.PictureId)},
                {"Deleted",false}
            };
                UnitOfWork.Repository<Group>().UpdateAsync(document, ObjectId.Parse(id), "groups");
                return new OkObjectResult(group.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Group group = (Group)this.GetById(id).Result;
            try
            {
                var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(group.StudentsId)},
                { "TeachersId", new BsonArray(group.TeachersId)},
                { "Name", "" + group.Name},
                { "Info", "" + group.Info},
                { "PictureId", ObjectId.Parse(group.PictureId)},
                { "Deleted", true}
            };
                UnitOfWork.Repository<Group>().DeleteAsync(document, ObjectId.Parse(id), "groups", true);
                return new OkObjectResult(group);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}
