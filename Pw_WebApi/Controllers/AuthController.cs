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
        throw new NotImplementedException();
    }
    
    // POST: api/Login   
    //[AllowAnonymous]
    [HttpPost(nameof(Register))]
    public ActionResult<TokenDto> Register([FromBody] LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

}