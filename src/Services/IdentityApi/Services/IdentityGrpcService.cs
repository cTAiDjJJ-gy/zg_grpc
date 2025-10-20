using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Protocol.Identity;
using Grpc.Protocol.User;
using IdentityApi.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Services
{
    public class IdentityGrpcService(ILogger<IdentityGrpcService> logger,
        //IConfiguration configuration,
        IdentityApiContext dbContext,
        UserGrpc.UserGrpcClient client) : IdentityGrpc.IdentityGrpcBase
    {
        public override async Task<RegisterReply> Register(RegisterRequest request, ServerCallContext context)
        {
            logger.LogInformation("Register Account Start!");

            var item = await dbContext.Identities.SingleOrDefaultAsync(i => i.UserName == request.UserName);
            if (item != null) return new RegisterReply
            {
                Message = "Account existed"
            };

            item = new Entities.Identity { UserName = request.UserName, Password = request.Password };
            dbContext.Identities.Add(item);

            await dbContext.SaveChangesAsync();

            //var channel = GrpcChannel.ForAddress(configuration["UserGrpcHost"]);
            //var client = new UserGrpc.UserGrpcClient(channel);

            var response = await client.CreateUserAsync(new CreateUserRequest { Name = request.UserName });
            logger.LogInformation($"Create User Result: {response.Code}");

            return new RegisterReply
            {
                Message = "Register Success! Hello " + request.UserName
            };
        }
    }
}
