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
    public class SchoolAdminsController : BaseController
    {
        public SchoolAdminsController(IConfiguration config, ILogger<SchoolAdminsController> logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<SchoolAdmin>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<SchoolAdmin>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SchoolAdminViewModel model)
        {
            try
            {

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };


                ILogger<UsersController> logger = new LoggerFactory().CreateLogger<UsersController>();
                OkObjectResult userResponse = (OkObjectResult)await new UsersController(Config, logger, DataContext).Post(user);

                var schoolAdmin = new SchoolAdmin
                {
                    CoursesId = model.CoursesId,
                    Deleted = false,
                    GroupsId = model.GroupsId,
                    TeachersId = model.TeachersId,
                    Type = model.Type,
                    UserId = Convert.ToString(userResponse.Value),
                    SchoolId = model.SchoolId
                };

                await UnitOfWork.Repository<SchoolAdmin>().InsertAsync(schoolAdmin);

                return new OkObjectResult(schoolAdmin.Id);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] SchoolAdmin schooladmin)
        {

            var document = new BsonDocument
            {
                 { "UserId", schooladmin.UserId},
                 { "TeachersId", new BsonArray(schooladmin.TeachersId)},
                 { "CoursesId",new BsonArray(schooladmin.CoursesId)},
                 { "GroupsId", new BsonArray(schooladmin.GroupsId)},
                 {"Type",schooladmin.Type },
                 {"Deleted",false},
                 {"SchoolId", ObjectId.Parse(schooladmin.SchoolId) }

            };
            try
            {
                UnitOfWork.Repository<SchoolAdmin>().UpdateAsync(document, ObjectId.Parse(id), "schooladmins");
                return new OkObjectResult(schooladmin.Id);
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
                SchoolAdmin schooladmin = (SchoolAdmin)this.GetById(id).Result;
                var document = new BsonDocument
            {
                 { "UserId", schooladmin.UserId},
                 { "TeachersId", new BsonArray(schooladmin.TeachersId)},
                 { "CoursesId",new BsonArray(schooladmin.CoursesId)},
                 { "GroupsId", new BsonArray(schooladmin.GroupsId)},
                 {"Type",schooladmin.Type },
                 {"Deleted",true },
                 {"SchoolId", ObjectId.Parse(schooladmin.SchoolId) }

            };
                UnitOfWork.Repository<SchoolAdmin>().DeleteAsync(document, ObjectId.Parse(id), "schooladmins", true);
                return new OkObjectResult(schooladmin);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}