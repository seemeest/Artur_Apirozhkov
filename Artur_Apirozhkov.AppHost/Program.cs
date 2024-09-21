var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Artur_Apirozhkov>("artur-apirozhkov");

builder.Build().Run();
