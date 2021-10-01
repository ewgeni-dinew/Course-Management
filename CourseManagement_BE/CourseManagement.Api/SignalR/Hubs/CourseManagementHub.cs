namespace CourseManagement.Api.SignalR.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class CourseManagementHub : Hub
    {
        public async Task RemoveViewer(object data)
        {
            // 'data' argument should contain both 'CourseId' and 'UserId'
            await Clients.All.SendAsync("removeViewer", data);
        }

        public async Task RemoveViewerOnLogout(object data)
        {
            // 'data' argument should contain only 'UserId'
            await Clients.All.SendAsync("removeViewerOnLogout", data);
        }
    }
}
