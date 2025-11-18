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
    [HttpGet("load-credits")]
    public IActionResult LoadCredits()
    {
        var result = creditsCommandManager.LoadCredits();
        return Ok(result);
    }
    [HttpGet("load-creditStatuses")]
    public IActionResult LoadCreditStatuses()
    {
        var result = creditsCommandManager.LoadCreditStatuses();
        return Ok(result);
    }
    [HttpGet("filter-credit-by-status")]
    public IActionResult FilterCreditsByStatus(int statusId)
    {
        var result = creditsCommandManager.FilterCreditsByStatus(statusId);
        return Ok(result);
    }
}
