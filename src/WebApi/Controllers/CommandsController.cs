using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CommandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("single")]
        public async Task PostSingleAsync()
        {
            await mediator.Send(new SingleCommand());
        }

        [HttpPost("group")]
        public async Task PostGroupAsync()
        {
            await mediator.Send(new GroupCommand());
        }
    }
}