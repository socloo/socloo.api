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
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Document> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Document>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Document doc)
        {

            await UnitOfWork.Repository<Document>().InsertAsync(doc);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Document doc)
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
            Document doc = this.GetById(id).Result;
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}