using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pw_Security.IServices;

using Pw_WebApi.Dtos.AuthDto;
using Pw_WebApi.Exceptions;

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
    
    [AllowAnonymous] 
    [HttpPost(nameof(Login))]
    public ActionResult<TokenDto> Login([FromBody] LoginDto loginDto)
    {
        var token = _securityService.GenerateJwtToken(loginDto.Email, loginDto.Password, out int userId);
        if (userId == -1)
            throw new RestException(HttpStatusCode.BadRequest, "Email or password are incorrect!");
        return new TokenDto
        {
            Jwt = token.Jwt,
            Message = token.Message,
            UserId = userId,
        };
    }
    
    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public ActionResult<TokenDto> Register([FromBody] LoginDto loginDto)
    {
        var exists = _securityService.EmailExists(loginDto.Email);
        if(exists)
            throw new RestException(HttpStatusCode.BadRequest, "Email is already in use!");
        if (_securityService.Create(loginDto.Email, loginDto.Password))
        {
            var token = _securityService.GenerateJwtToken(loginDto.Email, loginDto.Password, out int userId);
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