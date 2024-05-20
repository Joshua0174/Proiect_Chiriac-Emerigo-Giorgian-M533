using BusinessLayer.Contracts;
using BusinessLayer.Dto.Account;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService, SignInManager<AppUser> signInManager, ILogger<AccountController> logger )
        {
            _userManager = userManager; 
            _tokenService = tokenService;
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginDto logindto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == logindto.UserName.ToLower());
            
            if (user == null) { return Unauthorized("Invalid username!"); }
            var result = await _signInManager.CheckPasswordSignInAsync(user, logindto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect ");
           
            var roles = await _userManager.GetRolesAsync(user);

            
            var token = roles.Contains("Admin") ? _tokenService.CreateTokenForAdmin(user) : _tokenService.CreateTokenForUser(user);

            _logger.LogInformation("A logging event occurred");
            return Ok(
                 new NewUserDto
                 {
                     UserName = user.UserName,
                     Email = user.Email,
                     Token = token
                 }
                ); 
        }


        [HttpPost("registerAsUser")]
        public async Task<IActionResult> RegisterAsUser([FromBody]RegisterDto registerDto)
        {
            try{
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser=await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        _logger.LogInformation("An account registration has occurred");
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateTokenForUser(appUser)
                            }
                            );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPost("registerAsAdmin")]
        public async Task<IActionResult> RegisterAsAdmin([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                    if (roleResult.Succeeded)
                    {
                        _logger.LogInformation("An account registration has occurred");
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateTokenForAdmin(appUser)
                            }
                            );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
