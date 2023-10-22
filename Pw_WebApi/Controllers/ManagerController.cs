using System.ComponentModel.DataAnnotations;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pw_WebApi.BindingModels;
using Pw_WebApi.Dtos.ManagerDto;
namespace Pw_WebApi.Controllers;

[Authorize]
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
    public ActionResult GetAllByUserId(int userId) {
        return Ok(_service.GetAllPasswordsByUserId(userId).Select(pass => new PasswordDto(pass)));
    }

    [HttpGet("GenerateRandomPassword/{length:int}")]
    public ActionResult GenerateRandomPassword([Range(7, 64)] int length) {
        return Ok(_service.RandomPasswordGenerator(length));
    }

    [HttpPost("CreateNewPassword")]
    public ActionResult CreateNewPassword([FromBody] [Required] PasswordPostBindingModel model) {
        return Ok(
            new PasswordDto(_service.Create(new PasswordModel {
                WebsiteName = model.WebsiteName,
                Email = model.Email,
                Password = model.Password,
                User = new User{Id = model.UserId}
        })));
    }
    
    [HttpDelete("DeletePassword/{passwordId}")]
    public ActionResult Delete(int passwordId) {
        _service.Delete(passwordId);
        return Ok();
    }
    
    [HttpPut("UpdatePassword")]
    public ActionResult Update([Required] [FromBody] PasswordPutBindingModel model) {
        return Ok(
            new PasswordDto(_service.Update(new PasswordModel{
            Id = model.Id,
            WebsiteName = model.WebsiteName,
            Email = model.Email,
            Password = model.Password
        })));
    }
    
    [HttpGet("GetPasswordById/{passwordId}")]
    public ActionResult GetPasswordById(int passwordId) {
        return Ok(new PasswordDto(_service.GetPasswordById(passwordId)));
    }
}