using StudentManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Process;
using StudentManagementSystem.Model;
using static StudentManagementSystem.Providers.AccessProviders;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles =nameof(SystemUserType.User)),Route("api/[controller]")]
    public class TeacherController : BaseController
    {
        readonly  TeacherProcess process;
        public TeacherController([FromServices] User user) { process = new() { CurrentUser = user }; }
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(),true);
        [HttpPost] public async Task<IActionResult> Post([FromBody] Teacher data) => SendResponse(await process.Create(data), true);
        [HttpPut] public async Task<IActionResult> Put([FromBody] Teacher data) => SendResponse(await process.Update(data),true);
        [HttpDelete] public async Task<IActionResult> Delete(int id) => SendResponse(await process.Delete(id),  true);
    }
}
