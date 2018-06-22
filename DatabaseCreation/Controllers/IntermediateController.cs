using DatabaseCreation.Constants;
using DatabaseCreation.Interfaces;
using DatabaseCreation.Models.DTOs;
using DatabaseCreation.Services;
using System.Web.Http;

namespace DatabaseCreation.Controllers
{
    public class IntermediateController : ApiController
    {
        private readonly IDatabaseService databaseService;
        private readonly IStatusService statusService;

        public IntermediateController()
        {
            databaseService = DatabaseService.GetInstance();
            statusService = StatusService.GetInstance();
        }

        [HttpPost]
        [ActionName("Status")]
        public void CatchStatusOfCreation(StatusDTO creation)
        {
            statusService.Update(creation.DatabaseName, creation.Status);

            if (creation.Status == StatusValue.Success)
            {
                statusService.Update(creation.DatabaseName, StatusValue.UserCreation);
                databaseService.AddUser(creation.DatabaseName);
            }
        }
    }
}
