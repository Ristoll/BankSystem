using AutoMapper;
using BLL.Commands.BranchesCommands;
using Microsoft.AspNetCore.Mvc;
using DTO;

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

    [HttpDelete("delete-branch/{branchId}")]
    public IActionResult DeleteBranch(int branchId)
    {
        var result = branchesCommandManager.DeleteBranch(branchId);
        return Ok(result);
    }
}