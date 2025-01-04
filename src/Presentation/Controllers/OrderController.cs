using Application.Orders.Commands;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Queries;
using Application.Orders.Queries.GetAllOrders;
using Application.Orders.Queries.GetSingleOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[Route("[controller]")]
public class OrderController(
    ISender sender,
    ILogger<OrderController> logger) : WebApiControllerBase(sender)
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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all orders");
        var query = new GetAllOrdersQuery();
        var queryResult = await Sender.Send(query, cancellationToken);
        return queryResult.IsSuccessful ? Ok(queryResult.Value) : BadRequest(queryResult.Error);
    }
    
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all orders");
        var query = new GetSingleOrderQuery(orderId);
        var queryResult = await Sender.Send(query, cancellationToken);
        return queryResult.IsSuccessful ? Ok(queryResult.Value) : BadRequest(queryResult.Error);
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(Guid orderId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting order with id: {Id}", orderId);
        var cmd = new DeleteOrderCommand(orderId);
        var result = await Sender.Send(cmd, cancellationToken);
        return result.IsSuccessful ? Ok() : BadRequest(result.Error);
    }
}