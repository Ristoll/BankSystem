using AutoMapper;
using BLL.Commands.PaymentsCommands;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : Controller
{
    private readonly PaymentsCommandManager paymentsCommandManager;
    private readonly IMapper mapper;
    public PaymentsController(PaymentsCommandManager paymentsCommandManager, IMapper mapper)
    {
        this.paymentsCommandManager = paymentsCommandManager;
        this.mapper = mapper;
    }
    [HttpPost("add-payment")]
    public IActionResult AddPayment([FromBody] PaymentDto paymentDto)
    {
        var result = paymentsCommandManager.AddPayment(paymentDto);
        return Ok(result);
    }
    [HttpGet("load-payments")]
    public IActionResult LoadPayments()
    {
        var result = paymentsCommandManager.LoadPayments();
        return Ok(result);
    }
    [HttpGet("load-paymentTypes")]
    public IActionResult LoadPaymentTypes()
    {
        var result = paymentsCommandManager.LoadPaymentTypes();
        return Ok(result);
    }
}
