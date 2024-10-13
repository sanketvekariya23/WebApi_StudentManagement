using StudentManagementSystem.Model;
using StudentManagementSystem.Providers;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using static StudentManagementSystem.Providers.AccessProviders;
using System.Security.Claims;

namespace StudentManagementSystem.Process
{
    public class LoginProcess
    {
        public static async Task<ApiResponse> Login(AuthModel authModel)
        {
            var apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success};
            try
            {
                DefaultContext defualtcontext = new();
                User user = await defualtcontext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Username == authModel.UserName && u.Password == EncryptionProviders.Encrypt(authModel.PassWord));
                if (user == null)
                {
                    apiResponse.Message = "Enter valid Details;"; return apiResponse;
                }
                else
                {
                    user.Password = ""; DateTime expiry = DateTime.Now.Add(TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay);
                    Claim additionalClaim = new(ClaimTypes.Role, user.IsTeacher ? Convert.ToString(SystemUserType.Admin) : Convert.ToString(SystemUserType.User));
                    user.AccessToken = GetUserAccessToken(user, expiry, additionalClaim);
                    apiResponse.data = user; apiResponse.Status = (byte)StatusFlag.Success;
                    return (apiResponse);
                }
            }
            catch (Exception ex) { apiResponse.DetailedError = Convert.ToString(ex); return apiResponse; }
            return apiResponse;
        }
        public static async Task<ApiResponse> Register(User data)
        {
            var apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success};
            try
            {
                DefaultContext defualtcontext = new();
                if (defualtcontext.User.Any(u=>u.Username == data.Username)){ apiResponse.Message = "User already register "; }
                else if(defualtcontext.User.Any(u=>u.ContactNo == data.ContactNo)) { apiResponse.Message = "Mobile number already register "; }
                else
                {
                    data.IsTeacher = false;
                    data.Password = EncryptionProviders.Encrypt(data.Password);
                    await defualtcontext.User.AddAsync(data);
                    await defualtcontext.SaveChangesAsync();
                    apiResponse.Status = (Byte)StatusFlag.Success; apiResponse.Message = "Register Successfully";
                }
                await defualtcontext.SaveChangesAsync(); 
            }
            catch (Exception ex) { apiResponse.Status = (Byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); }
            return apiResponse;
        }
    }
}
