using Artur_Apirozhkov.VkApiCore.Services;
using Microsoft.AspNetCore.SignalR;

namespace Artur_Apirozhkov.Hubs
{
    public class ParserHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connect");
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception) { 

            Console.WriteLine("Disconnected");
            return base.OnDisconnectedAsync(exception);
        }

        public void SendAIphoto()
        {
            string photourl = " ";

            //Clients.All.SendAsync();
        }

        public async Task<string> SetPhotoDescription(string text,int id)
        {



            return string.Empty;
        }
    }
}
