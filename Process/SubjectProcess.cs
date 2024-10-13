using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentManagementSystem.Process
{
    public class SubjectProcess:GlobalVeriable
    {
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                apiResponse.data = defaultContext.Subject.AsNoTracking().ToList();                
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in get Subject data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Subject data)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var subjectexist = await defaultContext.Subject.AsNoTracking().FirstOrDefaultAsync(t => t.SubjectId == data.SubjectId);
                if (subjectexist == null)
                {
                    await defaultContext.Subject.AddAsync(data);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Message = "Subject Already present with subjectId";
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Creating Subject data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Update(Subject data)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var subjectexist = await defaultContext.Subject.FindAsync(data.SId);
                if (subjectexist != null)
                {
                    subjectexist.SubjectId = data.SubjectId;
                    subjectexist.SubjectName = data.SubjectName;
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "Subject not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating Subject data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int id)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                Subject subjectexist = defaultContext.Subject.AsNoTracking().FirstOrDefault(t => t.SId == id);
                if (subjectexist != null)
                {
                    defaultContext.Subject.Remove(subjectexist);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "user not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in get Deleting data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
    }
}
