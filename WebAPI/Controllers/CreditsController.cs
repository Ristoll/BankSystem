using AutoMapper;
using BLL.Commands.CreditsCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditsController : Controller
{
    private readonly CreditsCommandManager creditsCommandManager;
    private readonly IMapper mapper;
    public CreditsController(CreditsCommandManager creditsCommandManager, IMapper mapper)
    {
        this.creditsCommandManager = creditsCommandManager;
        this.mapper = mapper;
    }
    [HttpPost("add-credit")]
    public IActionResult AddCredit([FromBody] CreditDto creditDto)
    {
        var result = creditsCommandManager.AddCredit(creditDto);
        return Ok(result);
    }
    [HttpPut("update-credit")]
    public IActionResult UpdateCredit([FromBody] CreditDto creditDto)
    {
        var result = creditsCommandManager.UpdateCredit(creditDto);
        return Ok(result);
    }
    [HttpGet("filter-credit-by-status")]
    public IActionResult FilterCreditsByStatus([FromQuery] CreditStatusDto statusDto)
    {
        var result = creditsCommandManager.FilterCreditsByStatus(statusDto);
        return Ok(result);
    }
}
