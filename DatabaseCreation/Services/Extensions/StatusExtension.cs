using DatabaseCreation.Constants;
using DatabaseCreation.Models;

namespace DatabaseCreation.Services.Extensions
{
    public static class StatusExtension
    {
        public static bool IsDeployed(this StatusInfo status)
        {
            return status.CurrentStatus != StatusValue.New || status.CurrentStatus != StatusValue.CreationFailed;
        }

        public static bool HasUser(this StatusInfo status)
        {
            return status.CurrentStatus == StatusValue.UserCreationFailed || status.CurrentStatus == StatusValue.CreationComplete;
        }
    }
}