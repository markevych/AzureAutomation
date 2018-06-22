using DatabaseCreation.Services.Extensions;
using DatabaseCreation.Models;
using DatabaseCreation.Services;
using System;
using System.Web.Http;
using DatabaseCreation.Interfaces;

namespace DatabaseCreation.Controllers
{
    public class DatabaseController : ApiController
    {
        private readonly IDatabaseService databaseService;
        private readonly IStatusService statusService;

        public DatabaseController()
        {
            databaseService = DatabaseService.GetInstance();
            statusService = StatusService.GetInstance();
        }

        [HttpPost]
        [ActionName("new")]
        public IHttpActionResult Create(DatabaseInfo databaseInfo)
        {
            if(databaseInfo == null)
            {
                return BadRequest("The problem is that database info is null");
            }

            databaseService.Add(databaseInfo);
            return Ok("The process of creation has started");            
        }

        [HttpPost]
        [ActionName("deploy")]
        public IHttpActionResult Deploy(string databaseName)
        {
            try
            {
                databaseService.Create(databaseName);
                return Ok("The process was successfully started");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [ActionName("edit")]
        public IHttpActionResult Edit(DatabaseInfo databaseInfo)
        {
            var status = statusService.Get(databaseInfo.Name);
            if (status == null)
            {
                return NotFound();
            }

            if(status.IsDeployed())
            {
                try
                {
                    databaseService.Edit(databaseInfo);
                    return Ok("The database information was edited");
                }
                catch
                {
                    return BadRequest("The problem with editing");
                }
            }

            return BadRequest("The database is deployed");
        }

        [HttpPatch]
        [ActionName("regenerate-user")]
        public IHttpActionResult RegenerateUser(string databaseName)
        {
            var status = statusService.Get(databaseName);
            if (status == null)
            {
                return NotFound();
            }

            if (status.HasUser())
            {
                databaseService.AddUser(databaseName);
                return Ok("The user was regenerated");
            }

            return BadRequest($"User wasn't regenerated becasuse statuse \"{status.CurrentStatus}\" doesn't allow this.");            
        }

        [HttpGet]
        [ActionName("status")]
        public IHttpActionResult GetCurrentStatus(string databaseName)
        {
            var status = statusService.Get(databaseName);
            if (status != null)
            {
                return Ok(status);
            }
            return NotFound();
        }
    }
}
