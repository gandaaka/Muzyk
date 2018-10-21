using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Muzyk_API.Hubs
{
    public class MessageHub: Hub
    {
        public Task NewMessage(string message){
            //var sender = Context.User.Identity.Name;
            return Clients.Caller.SendAsync("NewMessage", message);
        }
    }
}