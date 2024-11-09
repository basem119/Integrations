using Integrations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integrations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController(JokeService jokeService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetJoke() => Ok(await jokeService.GetJokeAsync());
    }
}
