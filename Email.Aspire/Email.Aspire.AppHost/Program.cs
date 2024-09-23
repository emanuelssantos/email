var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Email_Consumer>("email-consumer").WithExternalHttpEndpoints();
builder.AddProject<Projects.Email_Producer>("email-producer").WithExternalHttpEndpoints();

builder.Build().Run();
