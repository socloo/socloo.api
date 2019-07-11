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
                return new BadRequestResult();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Assignment>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Assignment assignment)
        {
            try
            {
                await UnitOfWork.Repository<Assignment>().InsertAsync(assignment);



                return new OkObjectResult(assignment.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
            
        }


        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Assignment assignment)
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

                UnitOfWork.Repository<Assignment>().UpdateAsync(document, ObjectId.Parse(id), "assignments");
                return new OkObjectResult(assignment.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Assignment assignment = (Assignment)this.GetById(id).Result;
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
                return new OkObjectResult(assignment);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}