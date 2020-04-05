using System.Threading.Tasks;
using GTSLogGeneratorApi.Application.GetConfigRequest;
using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GTSLogGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetConfig")]
        public async Task<ActionResult<GetConfigResponse>> GetConfig()
        {
            return await _mediator.Send(new GetConfigRequest());            
        }
        
        [HttpPut]
        [Route("UpdateConfig")]
        public async Task<ActionResult> UpdateConfig(UpdateConfigRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}