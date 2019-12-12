using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace UnoWebApiSwagger.WebApi
{
    public class ChecksHub : Hub
    {
        private string UserRole => Context.GetHttpContext().Request.Query["userRole"];
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserRole);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserRole);
            await base.OnDisconnectedAsync(exception);
        }

    }
}