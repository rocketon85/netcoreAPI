using netcoreAPI.Identity;
using netcoreAPI.Models;
using netcoreAPI.Repository;
using System.Collections;
using System.Security.Claims;

namespace netcoreAPI.Services
{
    public class UserService: IUserService
    {
        private readonly IJwtService jwtService;
        private readonly UserRepository userRepository;

        public UserService(IJwtService jwtService, UserRepository userRepository)
        {
            this.jwtService = jwtService;
            this.userRepository = userRepository;
        }

        public async Task<AuthRespModel?> Authenticate(AuthRequest request)
        {
            var user = await this.userRepository.Get(request.Username, request.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = jwtService.GenerateJwtToken(user, await GetClaims(user));

            return await Task<AuthRespModel>.FromResult(new AuthRespModel ( user.Id, token ));
        }

        public async Task<User?> GetById(int id)
        {
            return await this.userRepository.GetById(id);
        }

        public async Task<Claim[]> GetClaims(User user)
        {
            if(user.Name.ToLower() == "admin")
            {
                return await Task<Claim[]>.FromResult(new []
                {
                    new Claim(ClaimTypes.Role, "admin")
                });
            }
            else
            {
                return await Task<Claim[]>.FromResult(new[]
                {
                    new Claim(ClaimTypes.Role, "user")
                });
            }
        }
    }
}
