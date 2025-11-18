using AutoMapper;
using BLL.Commands.AccountsCommands;
using BLL.Commands.BranchesCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BranchesController : Controller
{
    private readonly BranchesCommandManager branchesCommandManager;
    private readonly IMapper mapper;

    public BranchesController(BranchesCommandManager branchesCommandManager, IMapper mapper)
    {
        this.branchesCommandManager = branchesCommandManager;
        this.mapper = mapper;
    }

    [HttpPost("add-branch")]
    public IActionResult AddBranch([FromBody] BankBranchDto branchDto)
    {
        var result = branchesCommandManager.AddBranch(branchDto);
        return Ok(result);
    }

    [HttpPut("update-branch")]
    public IActionResult UpdateBranch([FromBody] BankBranchDto branchDto)
    {
        var result = branchesCommandManager.UpdateBranch(branchDto);
        return Ok(result);
    }

    [HttpGet("load-branches")]
    public IActionResult LoadBranches()
    {
        var result = branchesCommandManager.LoadBranches();
        return Ok(result);
    }
    [HttpGet("load-branchTypes")]
    public IActionResult LoadBranchTypes()
    {
        var result = branchesCommandManager.LoadBranchTypes();
        return Ok(result);
    }
    [HttpDelete("delete-branch/{branchId}")]
    public IActionResult DeleteBranch(int branchId)
    {
        var result = branchesCommandManager.DeleteBranch(branchId);
        return Ok(result);
    }
}