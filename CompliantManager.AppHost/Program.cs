var builder = DistributedApplication.CreateBuilder(args);

var sql = builder
    .AddAzureSqlServer("sqlserver")
    .RunAsContainer()
    .AddDatabase("Database");

builder.AddProject<Projects.CompliantManager_Server>("server")
    .WithReference(sql)
    .WaitFor(sql);

builder.Build().Run();