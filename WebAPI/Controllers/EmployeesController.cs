using AutoMapper;
using BLL.Commands.EmployeesCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EmployeesController : Controller
{
    private readonly EmployeesCommandManager creditsCommandManager;
    private readonly IMapper mapper;
    public EmployeesController(EmployeesCommandManager creditsCommandManager, IMapper mapper)
    {
        this.creditsCommandManager = creditsCommandManager;
        this.mapper = mapper;
    }

    [HttpPost("add-employee")]
    public IActionResult AddEmployee([FromBody] EmployeeDto employeeDto)
    {
        var result = creditsCommandManager.AddEmployee(employeeDto);
        return Ok(result);
    }

    [HttpPut("update-employee")]
    public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeDto)
    {
        var result = creditsCommandManager.UpdateEmployee(employeeDto);
        return Ok(result);
    }

    [HttpDelete("delete-employee")]
    public IActionResult DeleteEmployee([FromQuery] int employeeId)
    {
        var result = creditsCommandManager.DeleteEmployee(employeeId);
        return Ok(result);
    }

    [HttpPost("login-employee")]
    public IActionResult LoginEmployee(string phone, string password)
    {
        var result = creditsCommandManager.LoginEmployee(phone, password);
        return Ok(result);
    }
}
