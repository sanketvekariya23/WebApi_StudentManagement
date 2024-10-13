using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Controllers;
using StudentManagementSystem.Data;
using StudentManagementSystem.Model;
using StudentManagementSystem.Process;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static StudentManagementSystem.Providers.AccessProviders;
using Azure;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Providers;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles =nameof(SystemUserType.User)),Route("api/[controller]")]
    public class StudentController : BaseController
    {
        readonly StudentProcess process;
        public StudentController([FromServices] User user, CalculateGrad grad){ process = new StudentProcess(grad) { CurrentUser = user }; }
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(),true);
        [HttpPost] public async Task<IActionResult> Post([FromBody] Student std) => SendResponse(await process.Create(std),true);
        [HttpPut] public async Task<IActionResult> Put([FromBody] Student std) => SendResponse(await process.Update(std), true);
        [HttpDelete] public async Task<IActionResult> Delete(int id) => SendResponse(await process.Delete(id),true);
    }
}