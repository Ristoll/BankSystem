using AutoMapper;
using BLL.Commands.AccountsCommands;
using BLL.Commands.ClientsCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : Controller
{
    private readonly ClientsCommandManager clientsCommandManager;
    private readonly IMapper mapper;
    public ClientsController(ClientsCommandManager clientsCommandManager, IMapper mapper)
    {
        this.clientsCommandManager = clientsCommandManager;
        this.mapper = mapper;
    }
    [HttpPost("add-client")]
    public IActionResult AddClient([FromBody] ClientDto clientDto)
    {
        var result = clientsCommandManager.AddClient(clientDto);
        return Ok(result);
    }
    [HttpPut("update-client")]
    public IActionResult UpdateClient([FromBody] ClientDto clientDto)
    {
        var result = clientsCommandManager.UpdateClient(clientDto);
        return Ok(result);
    }
    [HttpGet("load-clients")]
    public IActionResult LoadClients()
    {
        var result = clientsCommandManager.LoadClients();
        return Ok(result);
    }
    [HttpGet("search-client-by-fullname")]
    public IActionResult SearchClientByFullName([FromQuery] string clientName)
    {
        var result = clientsCommandManager.SearchClientByFullName(clientName);
        return Ok(result);
    }
    [HttpGet("search-client-by-phonenumber")]
    public IActionResult SearchClientByPhoneNumber([FromQuery] string phoneNumber)
    {
        var result = clientsCommandManager.SearchClientByPhoneNumber(phoneNumber);
        return Ok(result);
    }
    [HttpGet("filter-client-by-accounttype")]
    public IActionResult FilterClientsByAccountType(int accountTypeId)
    {
        var result = clientsCommandManager.FilterClientsByAccountType(accountTypeId);
        return Ok(result);
    }
}
