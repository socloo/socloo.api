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
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Group> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Group>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Group group)
        {

            await UnitOfWork.Repository<Group>().InsertAsync(group);

            return true;
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string _id, [FromBody] Group group)
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
            };
                UnitOfWork.Repository<Group>().Update(document, ObjectId.Parse(_id), "groups");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] Group group)
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
            };
                UnitOfWork.Repository<Group>().Delete(document, ObjectId.Parse(id), "groups", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
