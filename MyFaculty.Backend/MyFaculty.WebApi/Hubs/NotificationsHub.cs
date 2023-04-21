using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MyFaculty.WebApi.Hubs.Management;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationsHub : Hub
    {
        private readonly OnlineUsers _onlineUsers;

        public NotificationsHub(OnlineUsers onlineUsers)
        {
            _onlineUsers = onlineUsers;
        }

        public override Task OnConnectedAsync()
        {
            int requesterId = int.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string connectionId = Context.ConnectionId;
            _onlineUsers.AddUser(requesterId, connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            int requesterId = int.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _onlineUsers.Remove(requesterId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task MakeUserLoadNotificationsAsync(int userId)
        {
            string connectionId = _onlineUsers.GetConnectionIdByRealId(userId);
            if (!string.IsNullOrEmpty(connectionId))
                await Clients.Client(connectionId).SendAsync("loadNotifications");
        } 
    }
}
