using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
public class WebApiControllerBase(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}