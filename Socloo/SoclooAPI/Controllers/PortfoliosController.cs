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
    public class PorfoliosController : BaseController
    {
        public PorfoliosController(IConfiguration config, ILogger<PorfoliosController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Portfolio>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Portfolio> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Portfolio>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Portfolio porfolio)
        {

            await UnitOfWork.Repository<Portfolio>().InsertAsync(porfolio);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Portfolio portfolio)
        {
            
            try
            {
                var document = new BsonDocument
            {  { "UserId",ObjectId.Parse(portfolio.UserId)},
                { "Education", ""+portfolio.Education},
                { "Skills", ""+portfolio.Skills},
                { "Experience",""+ portfolio.Experience},
                { "Interests", ""+portfolio.Interests},
                { "References", ""+portfolio.References},
                { "GeneralInfo", ""+portfolio.GeneralInfo},
                { "Certification",""+portfolio.Certification}
            };
                UnitOfWork.Repository<Portfolio>().UpdateAsync(document, ObjectId.Parse(id), "portfolios");
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
                Portfolio portfolio= this.GetById(id).Result;
                var document = new BsonDocument
            {  { "UserId",ObjectId.Parse(portfolio.UserId)},
                { "Education", ""+portfolio.Education},
                { "Skills", ""+portfolio.Skills},
                { "Experience",""+ portfolio.Experience},
                { "Interests", ""+portfolio.Interests},
                { "References", ""+portfolio.References},
                { "GeneralInfo", ""+portfolio.GeneralInfo},
                { "Certification",""+portfolio.Certification},
                {"Deleted",true }
            };
                UnitOfWork.Repository<Portfolio>().DeleteAsync(document, ObjectId.Parse(id), "portfolios", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}