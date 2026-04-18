using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;       

        public UserService(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public Task CreateAsync(UserDto register)
        {
            register.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);
            return _userRepository.CreateAsync(register);
        }

        public async Task<string> LoginAsync(UserDto loginDto)
        {
            var user = await _userRepository.LoginAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = _tokenGenerator.GenerateToken(user.Id, user.Email);

            return token;
        }

    }
}
