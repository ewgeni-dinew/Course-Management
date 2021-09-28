namespace CourseManagement.Api.SignalR.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class CourseManagementHub: Hub
    {
        public async Task RemoveViewer(object data)
        {
            await Clients.All.SendAsync("removeViewer", data);
        }
    }
}
