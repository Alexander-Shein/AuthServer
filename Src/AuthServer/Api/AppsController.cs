using System;
using System.Threading.Tasks;
using AuthGuard.Services.Apps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class AppsController : Controller
    {
        readonly IAppsService appsService;

        public AppsController(IAppsService appsService)
        {
            this.appsService = appsService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string returnUrl)
        {
            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                return Ok(await appsService.GetAuthGuardApp());
            }

            return Ok(await appsService.Search(returnUrl));
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] ExtendedAppIm im)
        {
            return await Put(Guid.NewGuid(), im);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ExtendedAppIm im)
        {
            var result = await appsService.Put(id, im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.App);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await appsService.GetAll();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await appsService.Get(id);
            return Ok(result);
        }
    }
}