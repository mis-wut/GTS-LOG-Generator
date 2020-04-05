using GTSLogGeneratorApi.Application.GetConfigRequest;
using GTSLogGeneratorApi.Application.UpdateConfigRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GTSLogGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private IMediator _mediator;

        public ConfigurationController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route("UpdateConfig")]
        public ActionResult UpdateConfig(UpdateConfigRequest request)
        {
            return Ok();
        }
        
        [HttpPut]
        [Route("GetConfig")]
        public ActionResult GetConfig(GetConfigRequest request)
        {
            return Ok();
        }
    }
}