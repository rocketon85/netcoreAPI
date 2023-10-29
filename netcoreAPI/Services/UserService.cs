using netcoreAPI.Identity;
using netcoreAPI.Models;
using netcoreAPI.Repositories;
using System.Security.Claims;

namespace netcoreAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly UserRepository _userRepository;

        public UserService(IJwtService jwtService, UserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<AuthRespModel?> Authenticate(AuthRequest request)
        {
            var user = await _userRepository.Get(request.Username, request.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = _jwtService.GenerateJwtToken(user, await GetClaims(user));

            return await Task<AuthRespModel>.FromResult(new AuthRespModel(user.Id, token));
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<Claim[]> GetClaims(User user)
        {
            if (user.Name.ToLower() == "admin")
            {
                return await Task<Claim[]>.FromResult(new[]
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
