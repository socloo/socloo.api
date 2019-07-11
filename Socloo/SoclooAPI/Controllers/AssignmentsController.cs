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
    public class AssignmentsController : BaseController
    {
        public AssignmentsController(IConfiguration config, ILogger<AssignmentsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Assignment>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Assignment> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Assignment>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> Post([FromBody] Assignment assignment)
        {

            await UnitOfWork.Repository<Assignment>().InsertAsync(assignment);



            return true;
        }


        [HttpPut("{_id}")]
        async public Task<bool> Put(string _id, [FromBody] Assignment assignment)
        {

            try
            {
                var document = new BsonDocument
            {

                 { "TeachersId", new BsonArray(assignment.TeachersId)},
                 { "StudentsId", new BsonArray(assignment.StudentsId)},
                { "ExpirationDate",Convert.ToDateTime(assignment.ExpirationDate)},
                { "Info", assignment.Info},
                { "FileId", ObjectId.Parse(assignment.FileId)}
            };

                UnitOfWork.Repository<Assignment>().Update(document, ObjectId.Parse(_id), "assignments");
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
            Assignment assignment = this.GetById(id).Result;
            try
            {
                var document = new BsonDocument
            {
                 { "TeachersId", new BsonArray(assignment.TeachersId)},
                 { "StudentsId", new BsonArray(assignment.StudentsId)},
                { "ExpirationDate",Convert.ToDateTime(assignment.ExpirationDate)},
                { "Info", assignment.Info},
                { "FileId", ObjectId.Parse(assignment.FileId)},
                    { "Deleted", true}
            };
                UnitOfWork.Repository<Assignment>().DeleteAsync(document, ObjectId.Parse(id), "assignments", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}