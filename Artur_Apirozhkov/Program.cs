using Artur_Apirozhkov.BdModels;
using Artur_Apirozhkov.VkApiCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using VkNet;
using VkNet.Model;
//PS Ветка 3 потоки
//не забудь отдельно либу c++



VkApi api = new VkApi();

//Отключил брать из конфига

string AccessToken = "vk1.a.yp00MhbuZuKhghuSqH7vz4e4BF5hPVNtfPqJicjw674XGqZlU9qtMpAH5Ni_PNACFpc4WolU2sN5PMcLDxC2-3eSmBV6bi-qUgjOtIQIVCU9jJunksceewEZl7mT_bE4UFgqkEiQLN3fSfHh4CA5oqPfHW-m2tNNzxfECIk05v7GEL-AHCAXXnb526Klr9aksdky-ZQC_UhXgreuBcRqlQ";

api.Authorize(new ApiAuthParams
{
    AccessToken = AccessToken
});

Console.WriteLine("Успешная авторизация");

var userService = new UserService(api);

SearchServices search = new SearchServices(api);
search.Search();




// отключено так как не используем ии 

///// Часть ASP
//var builder = WebApplication.CreateBuilder(args);
//IConfiguration configuration = builder.Configuration;

//builder.AddServiceDefaults();

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Настройка API и маршрутизации
//app.MapDefaultEndpoints();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//// Запускаем сервер в основном потоке
//app.Run();
