using libreriaVirtual.Dtos;
using libreriaVirtual.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace libreriaVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;

    public UserController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateUser(RegisterDto userDTO)
    {
        if (ModelState.IsValid)
        {
            UserModel user = new UserModel
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
            };

            IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                return Ok($"User {user.UserName} created successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
  
    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users.ToList();
        return Ok(users);
    }
    
    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateUser(string userName, RegisterDto userDTO)
    {
        UserModel user = await _userManager.FindByNameAsync(userName);
        if (user != null)
        {
            user.UserName = userDTO.UserName;
            user.Email = userDTO.Email;
            user.PasswordHash = userDTO.Password;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok($"User {userName} updated successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        return NotFound($"User with Name {userName} not found");
    }
    
    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteUser(string userName)
    {
        UserModel user = await _userManager.FindByNameAsync(userName);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok($"User {userName} deleted successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        return NotFound($"User with Name {userName} not found");
    }
}