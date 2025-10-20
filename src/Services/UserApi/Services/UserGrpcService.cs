using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Grpc.Protocol.User;
using UserApi.Data;

namespace UserApi.Services
{
    public class UserGrpcService(ILogger<UserGrpcService> logger, UserApiContext dbContext) : UserGrpc.UserGrpcBase
    {
        public override async Task<BaseReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            logger.LogInformation("Create User Start!");

            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Name == request.Name);
            if (user != null) throw new Exception(nameof(user));

            user = new Entities.User { Name = request.Name };

            dbContext.Users.Add(user);

            await dbContext.SaveChangesAsync();

            return new BaseReply
            {
                Message = "Create User Success, Hello " + request.Name
            };
        }

        public override async Task<BaseReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            logger.LogInformation("Update User Start!");

            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Name == request.Name);
            if (user == null) return new BaseReply
            {
                Message = "User does not exist!"
            };

            user.BirthDate = request.BithDate.ToDateTime();
            user.Gender = request.Gender;
            user.PhoneNumber = request.PhoneNumber;
            user.Department = request.Department;

            await dbContext.SaveChangesAsync();

            return new BaseReply
            {
                Message = "User User Success"
            };
        }
    }
}
