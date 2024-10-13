using StudentManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Process;

namespace StudentManagementSystem.Controllers
{
    [ApiController, AllowAnonymous, Route("api/[controller]")]
    public class AuthController : BaseController
    {
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] AuthModel data) => SendResponse(await LoginProcess.Login(data),true);
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] User data) => SendResponse(await LoginProcess.Register(data), true);
    }
}
