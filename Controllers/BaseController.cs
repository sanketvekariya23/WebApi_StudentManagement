using StudentManagementSystem.Model;
using StudentManagementSystem.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Azure;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
   
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult SendResponse(ApiResponse apiResponse, bool ShowMessage = false)
        {
            if (ShowMessage) { apiResponse.Message ??= Convert.ToString(Enum.Parse<StatusFlag>(Convert.ToString(apiResponse.Status))).AddSpaceBeforeCapital(); }
            return apiResponse.Status == (byte)StatusFlag.Failed ? BadRequest(apiResponse) : Ok(apiResponse);
        }
    }
}