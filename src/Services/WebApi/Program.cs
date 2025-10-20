using Grpc.Protocol.Identity;
using Grpc.Protocol.User;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIÎÄµµ", Version = "v1" });
});

builder.Services.AddGrpcClient<UserGrpc.UserGrpcClient>(o =>
{
    o.Address = new Uri(builder.Configuration["UserGrpcHost"]);
});
builder.Services.AddGrpcClient<IdentityGrpc.IdentityGrpcClient>(o =>
{
    o.Address = new Uri(builder.Configuration["IdentityGrpcHost"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseHttpsRedirection();

app.MapPost("/register", async (RegisterRequest request, IdentityGrpc.IdentityGrpcClient client) =>
{
    //var channel = GrpcChannel.ForAddress(configuration["IdentityGrpcHost"]);
    //var client = new IdentityGrpc.IdentityGrpcClient(channel);

    var response = await client.RegisterAsync(request);

    Console.WriteLine($"Code: {response.Code} Result: {response.Message}");
});

app.MapPost("/user", async (UpdateUserRequest request, UserGrpc.UserGrpcClient client) =>
{
    //var channel = GrpcChannel.ForAddress(configuration["UserGrpcHost"]);
    //var client = new UserGrpc.UserGrpcClient(channel);

    var response = await client.UpdateUserAsync(request);

    Console.WriteLine($"Code: {response.Code} Result: {response.Message}");
});

app.Run();
