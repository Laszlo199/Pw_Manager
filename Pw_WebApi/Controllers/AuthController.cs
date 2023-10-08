using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pw_Security.IServices;
using Pw_WebApi.Dtos;

namespace Pw_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISecurityService _securityService;

    public AuthController(ISecurityService securityService)
    {
        _securityService = securityService;
    }
        
    // POST: api/Login
    //[AllowAnonymous] 
    [HttpPost(nameof(Login))]
    public ActionResult<TokenDto> Login([FromBody] LoginDto loginDto)
    {
        int userId; 
        var token = _securityService.GenerateJwtToken(loginDto.Email, loginDto.Password, out userId);
        return new TokenDto
        {
            Jwt = token.Jwt,
            Message = token.Message,
            UserId = userId,
        };
    }
    
    // POST: api/Login   
    //[AllowAnonymous]
    [HttpPost(nameof(Register))]
    public ActionResult<TokenDto> Register([FromBody] LoginDto loginDto)
    {
        var exists = _securityService.EmailExists(loginDto.Email);
        if(exists)
            return BadRequest("Email already exists!");
        if (_securityService.Create(loginDto.Email, loginDto.Password))
        {
            int userId;
            var token = _securityService.GenerateJwtToken(loginDto.Email, loginDto.Password, out userId);
            return new TokenDto
            {
                Jwt = token.Jwt,
                Message = token.Message,
                UserId = userId,
            };
        }
        return BadRequest("Something went wrong.");
    }

}