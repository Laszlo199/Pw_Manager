using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Pw_WebApi.Dtos;
using Pw_WebApi.Dtos.ManagerDto;

namespace Pw_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagerController: ControllerBase
{
    private readonly IManagerService _service;
    public ManagerController(IManagerService service)
    {
        _service = service;
    }
    [HttpGet("GetAllByUserId/{userId}")]
    public ActionResult<List<GetAllByUserIdDto>> GetAllByUserId(int userId)
    {
        try
        {
            return Ok(_service.GetAllPasswordsByUserId(userId)
                .Select(p => new GetAllByUserIdDto()
                {
                    WebsiteName = p.WebsiteName,
                    Email = p.Email,
                    Password = p.Password,
                }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GenerateRandomPassword/{lenght}")]
    public ActionResult GenerateRandomPassword(int lenght)
    {
        try
        {
            return Ok(_service.RandomPasswordGenerator(lenght));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("CreateNewPassword")]
    public ActionResult<Passwords> CreateNewPassword([FromBody] CreateNewPasswordDto postNewPasswordDto)
    {
        if (postNewPasswordDto == null)
            throw new InvalidDataException("deck cannot be null");
          
        try
        {
            return Ok(_service.Create(new Passwords()
            {
                Email = postNewPasswordDto.Email,
                WebsiteName = postNewPasswordDto.WebsiteName,
                Password = postNewPasswordDto.Password,
                User = new User{Id = postNewPasswordDto.UserId}
            }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("DeletePassword/{passwordId}")]
    public ActionResult<Passwords> Delete(int passwordId)
    {
        try
        {
            return Ok(_service.Delete(passwordId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("UpdatePassword")]
    public ActionResult<Passwords> Update([FromBody] UpdatePasswordDto passwordDto)
    {
        if (passwordDto == null) throw new InvalidDataException("Password to update cannot be null");
        try
        {
            return Ok(_service.Update(new Passwords()
            {
                Id = passwordDto.Id,
                Email = passwordDto.Email,
                WebsiteName = passwordDto.WebsiteName,
                Password = passwordDto.Password
            }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}