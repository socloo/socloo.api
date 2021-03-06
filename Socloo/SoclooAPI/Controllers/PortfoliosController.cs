﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
using System.Threading.Tasks;
namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : BaseController
    {
        public PortfoliosController(IConfiguration config, ILogger<PortfoliosController> logger, DataContext context) :
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

                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Portfolio>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Portfolio portfolio)
        {
            try
            {

                await UnitOfWork.Repository<Portfolio>().InsertAsync(portfolio);
                return new OkObjectResult(portfolio.Id);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Portfolio portfolio)
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
                { "Certification",""+portfolio.Certification},
                {"Deleted",false}
            };
                UnitOfWork.Repository<Portfolio>().UpdateAsync(document, ObjectId.Parse(id), "portfolios");
                return new OkObjectResult(portfolio.Id);
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
                Portfolio portfolio = (Portfolio)this.GetById(id).Result;
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
                return new OkObjectResult(portfolio);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}