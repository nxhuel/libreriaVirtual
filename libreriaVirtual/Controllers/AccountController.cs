using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using libreriaVirtual.Dtos;
using libreriaVirtual.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace libreriaVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IConfiguration _config;

    public AccountController(UserManager<UserModel> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Registe(RegisterDto userDTO)
    {
        if (ModelState.IsValid)
        {
            var isFirstUser = !_userManager.Users.Any();
            UserModel user = new UserModel()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
            };
            IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                if (isFirstUser)
                {
                    await _userManager.AddToRoleAsync(user, "admin"); // Primer usuario
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "user"); // Los demás
                }

                return Ok("Account Created");
            }

            return BadRequest(result.Errors);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto userDTO)
    {
        if (ModelState.IsValid)
        {
            UserModel? userFromDB = await _userManager.FindByNameAsync(userDTO.UserName);
            if (userFromDB != null)
            {
                bool found = await _userManager.CheckPasswordAsync(userFromDB, userDTO.Password);
                if (found)
                {
                    //Create Token
                    List<Claim> myclaims = new List<Claim>();
                    myclaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));
                    myclaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                    myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await _userManager.GetRolesAsync(userFromDB);
                    foreach (var role in roles)
                    {
                        myclaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var SignKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config["JWT:Key"]));

                    SigningCredentials signingCredentials =
                        new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken mytoken = new JwtSecurityToken(
                        issuer: _config["JWT:Issuer"], //provider create token
                        audience: _config["JWT:Audience"], //cousumer url
                        expires: DateTime.Now.AddHours(1),
                        claims: myclaims,
                        signingCredentials: signingCredentials);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expired = mytoken.ValidTo
                    });
                }
            }

            return BadRequest("Invalid Request");
        }

        return BadRequest(ModelState);
    }
}