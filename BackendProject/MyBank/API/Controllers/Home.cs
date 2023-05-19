using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class Home : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok("Welcome...");
        }
    }
}
