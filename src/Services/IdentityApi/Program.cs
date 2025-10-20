using Grpc.Protocol.User;
using IdentityApi.Data;
using IdentityApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcClient<UserGrpc.UserGrpcClient>(o =>
{
    o.Address = new Uri(builder.Configuration["UserGrpcHost"]);
});

builder.Services.AddDbContext<IdentityApiContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<IdentityGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
