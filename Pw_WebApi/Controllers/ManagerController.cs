using Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Pw_WebApi.Dtos;

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
    [HttpGet("GetAllPasswordsByUserId/{userId}")]
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
                    DateCreated = p.DateCreated
                }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}