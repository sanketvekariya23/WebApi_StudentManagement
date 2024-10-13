using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Model;
using StudentManagementSystem.Process;
using static StudentManagementSystem.Providers.AccessProviders;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles = nameof(SystemUserType.User)), Route("api/[controller]")]
    public class MarksController : BaseController
    {
        readonly MarksProces process;
        public MarksController([FromServices] User user) { process = new() { CurrentUser = user }; }
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(), true);
        [HttpPost] public async Task<IActionResult> Post([FromBody] Marks data) => SendResponse(await process.Create(data), true);
        [HttpPut] public async Task<IActionResult> Put([FromBody] Marks data) => SendResponse(await process.Update(data), true);
        [HttpDelete] public async Task<IActionResult> Delete(int id) => SendResponse(await process.Delete(id), true);
    }
}
