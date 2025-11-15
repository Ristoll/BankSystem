using AutoMapper;
using BLL.Commands.TransactionsCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : Controller
{
    private readonly TransactionsCommandManager transactionsCommandManager;
    private readonly IMapper mapper;
    public TransactionsController(TransactionsCommandManager transactionsCommandManager, IMapper mapper)
    {
        this.transactionsCommandManager = transactionsCommandManager;
        this.mapper = mapper;
    }
    [HttpPost("add-transaction")]
    public IActionResult AddTransaction([FromBody] TransactionDto transactionDto)
    {
        var result = transactionsCommandManager.AddTransaction(transactionDto);
        return Ok(result);
    }
    [HttpGet("search-transaction-by-period")]
    public IActionResult SearchTransactionByPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = transactionsCommandManager.SearchTransactionByPeriod(startDate, endDate);
        return Ok(result);
    }
}
