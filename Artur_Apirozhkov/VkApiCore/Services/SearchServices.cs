using Newtonsoft.Json;
using VkNet;
using VkNet.Enums.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using VkNet.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Google.Protobuf.WellKnownTypes;
using Artur_Apirozhkov.VkApiCore.Models;
using Microsoft.Identity.Client;

namespace Artur_Apirozhkov.VkApiCore.Services
{
    public class SearchServices
    {
        private readonly VkApi _api;

        private UserService UserService;
        public SearchServices(VkApi api)
        {
            _api = api;
            UserService = new UserService(api);

        }
        public void Search()
        {
            var users = _api.Users.Search(new UserSearchParams { Count=1000,AgeTo=22, Fields = ProfileFields.All ,Sex=VkNet.Enums.Sex.Unknown }).ToList();

            foreach (var user in users) {

                UserService.GetUserAsync(user.Id);
            }
        }

    
        

    }
}
