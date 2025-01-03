using Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[Route("[controller]")]
public class WeatherForecastController(
    ISender sender,
    ILogger<WeatherForecastController> logger) : WebApiControllerBase(sender)
{
    [HttpPost(Name = "CreateOrder")]
    public async Task<IActionResult> CreateAnOrder(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating an order");
        var created = await Sender.Send(command, cancellationToken);
        return created.IsSuccessful ? Ok() : BadRequest(created.Error);
    }
}