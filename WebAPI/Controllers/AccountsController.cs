using AutoMapper;
using BLL.Commands.AccountsCommands;
using Microsoft.AspNetCore.Mvc;
using DTO;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : Controller
{
    private readonly AccountsCommandManager accountsCommandManager;
    private readonly IMapper mapper;
    public AccountsController(AccountsCommandManager accountsCommandManager, IMapper mapper)
    {
        this.accountsCommandManager = accountsCommandManager;
        this.mapper = mapper;
    }
    [HttpPost("add-account")]
    public IActionResult AddAccount([FromBody] AccountDto accountDto)
    {
        var result = accountsCommandManager.AddAccount(accountDto);
        return Ok(result);
    }
    [HttpPut("update-account")]
    public IActionResult UpdateAccount([FromBody] AccountDto accountDto)
    {
        var result = accountsCommandManager.UpdateAccount(accountDto);
        return Ok(result);
    }
    [HttpGet("load-accounts")]
    public IActionResult LoadTransactions()
    {
        var result = accountsCommandManager.LoadAccounts();
        return Ok(result);
    }
    [HttpGet("load-accountTypes")]
    public IActionResult LoadAccountTypes()
    {
        var result = accountsCommandManager.LoadAccountTypes();
        return Ok(result);
    }
    [HttpGet("load-currencies")]
    public IActionResult LoadCurrencies()
    {
        var result = accountsCommandManager.LoadCurrencies();
        return Ok(result);
    }
    [HttpGet("filter-account-by-currency")]
    public IActionResult FilterAccountsByCurrency([FromQuery] CurrencyDto currencyDto)
    {
        var result = accountsCommandManager.FilterAccountsByCurrency(currencyDto);
        return Ok(result);
    }
    [HttpGet("filter-account-by-status")]
    public IActionResult FilterAccountsByStatus([FromQuery] bool status)
    {
        var result = accountsCommandManager.FilterAccountsByStatus(status);
        return Ok(result);
    }
    [HttpGet("search-account-by-owner")]
    public IActionResult FilterAccountsByOwner([FromQuery] string ownerName)
    {
        var result = accountsCommandManager.SearchAccountByOwnerName(ownerName);
        return Ok(result);
    }
}
