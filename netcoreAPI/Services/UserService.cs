using netcoreAPI.Identity;
using netcoreAPI.Models;
using netcoreAPI.Repository;

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

        public dynamic? Authenticate(AuthRequest request)
        {
            var user = this.userRepository.Get(request.Username, request.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = jwtService.GenerateJwtToken(user);

            return new { Token = token, UserId = user.Id};
        }

        public User? GetById(int id)
        {
            return this.userRepository.GetById(id);
        }
    }
}
