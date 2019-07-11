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
    public class DocumentsController : BaseController
    {
        public DocumentsController(IConfiguration config, ILogger<DocumentsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Document>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<Document>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Document doc)
        {
            try
            {
                await UnitOfWork.Repository<Document>().InsertAsync(doc);

                return new OkObjectResult(doc.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
            
        }


        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Document doc)
        {

            try
            {
                var document = new BsonDocument
            {
                 {"FileId",ObjectId.Parse(doc.FileId)  },
                 { "UsersId", new BsonArray(doc.UsersId)},
                 { "TeacherId", ObjectId.Parse(doc.TeacherId)},
                { "DateTime",doc.DateTime},
            };

                UnitOfWork.Repository<Document>().UpdateAsync(document, ObjectId.Parse(id), "documents");
                return new OkObjectResult(doc.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Document doc = (Document)this.GetById(id).Result;
            try
            {
                var document = new BsonDocument
            {
                 {"FileId",ObjectId.Parse(doc.FileId)  },
                 { "UsersId", new BsonArray(doc.UsersId)},
                 { "TeacherId", ObjectId.Parse(doc.TeacherId)},
                { "DateTime",doc.DateTime},
                { "Deleted", true}
            };
                UnitOfWork.Repository<Document>().DeleteAsync(document, ObjectId.Parse(id), "documents", true);
                return new OkObjectResult(doc);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}