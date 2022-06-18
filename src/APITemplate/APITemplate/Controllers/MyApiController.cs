using Microsoft.AspNetCore.Mvc;

namespace APITemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class MyApiController : ControllerBase
{
    private readonly ILogger<MyApiController> _logger;
    private readonly MyDbContext _repository;

    public MyApiController(ILogger<MyApiController> logger, MyDbContext repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("Action", Name = "ActionName")]
    public async Task<ActionResult> Get()
    {
        return Ok();
    }
}

