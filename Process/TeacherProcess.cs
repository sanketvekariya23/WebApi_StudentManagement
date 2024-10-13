using StudentManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentManagementSystem.Process
{
    public class TeacherProcess : GlobalVeriable
    {
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
               apiResponse.data = await defaultContext.Teacher.AsNoTracking().ToListAsync();
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in get Teacher data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Teacher data)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var teacherexist = await defaultContext.Teacher.AsNoTracking().FirstOrDefaultAsync(t=>t.Email == data.Email);
                if (teacherexist == null)
                {
                    await defaultContext.Teacher.AddAsync(data);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Message = "Email Id Already present";
                    apiResponse.Status= (Byte)StatusFlag.Failed;
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in creating teacher"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Update(Teacher data)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var teacherexist = await defaultContext.Teacher.FindAsync(data.TeacherId);
                if (teacherexist != null) 
                { 
                     teacherexist.TeacherName = data.TeacherName;
                     teacherexist.Email = data.Email;
                     teacherexist.SubjectProfessor = data.SubjectProfessor;
                    await defaultContext.SaveChangesAsync();    
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "user not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating teacher"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int teacherid)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                Teacher teacherexist = defaultContext.Teacher.AsNoTracking().FirstOrDefault(t => t.TeacherId == teacherid);
                if (teacherexist != null)
                {
                     defaultContext.Teacher.Remove(teacherexist);
                     await defaultContext.SaveChangesAsync();    
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "user not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating teacher"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
    }
}
