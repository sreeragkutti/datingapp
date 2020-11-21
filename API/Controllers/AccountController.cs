using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController 
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _dataContext = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto regiterDto)
        {
            if(await IsUserExists(regiterDto.Username))  return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
            
            var user = new AppUser() {
                Username = regiterDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(regiterDto.Password)),
                PasswordSalt = hmac.Key
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return Ok(new UserDto {Username = user.Username , Token = _tokenService.CreateToken(user)});
        } 
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
        {
            var user = await _dataContext.Users
                .SingleOrDefaultAsync(x=>x.Username == loginDto.Username);

            if (user == null) return Unauthorized("Invalid user");
            
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            
            return Ok(new UserDto {Username = user.Username , Token = _tokenService.CreateToken(user)});
        }

        private async Task<bool> IsUserExists(string username)
        {
            return await _dataContext.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}