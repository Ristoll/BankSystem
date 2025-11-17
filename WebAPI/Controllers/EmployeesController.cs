using AutoMapper;
using BLL.Commands.EmployeesCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EmployeesController : Controller
{
    private readonly EmployeesCommandManager employeesCommandManager;
    private readonly IMapper mapper;
    public EmployeesController(EmployeesCommandManager creditsCommandManager, IMapper mapper)
    {
        this.employeesCommandManager = creditsCommandManager;
        this.mapper = mapper;
    }

    [HttpPost("add-employee")]
    public IActionResult AddEmployee([FromBody] EmployeeDto employeeDto)
    {
        var result = employeesCommandManager.AddEmployee(employeeDto);
        return Ok(result);
    }

    [HttpPut("update-employee")]
    public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeDto)
    {
        var result = employeesCommandManager.UpdateEmployee(employeeDto);
        return Ok(result);
    }
    [HttpGet("load-employees")]
    public IActionResult LoadEmployees()
    {
        var result = employeesCommandManager.LoadEmployees();
        return Ok(result);
    }
    [HttpGet("load-employeeRoles")]
    public IActionResult LoadEmployeeRoles()
    {
        var result = employeesCommandManager.LoadEmployeeRoles();
        return Ok(result);
    }
    [HttpDelete("delete-employee")]
    public IActionResult DeleteEmployee([FromQuery] int employeeId)
    {
        var result = employeesCommandManager.DeleteEmployee(employeeId);
        return Ok(result);
    }

    [HttpPost("login-employee")]
    public IActionResult LoginEmployee(string phone, string password)
    {
        var result = employeesCommandManager.LoginEmployee(phone, password);
        return Ok(result);
    }
}
