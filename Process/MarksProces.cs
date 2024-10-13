using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Model;

namespace StudentManagementSystem.Process
{
    public class MarksProces: GlobalVeriable
    {
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                apiResponse.data = defaultContext.Mark.AsNoTracking().ToList();
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in get marks data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Marks data)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                data.Subject = null;
                data.Student = null;
                await defaultContext.Mark.AddAsync(data);
                await defaultContext.SaveChangesAsync();
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Creating marks data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Update(Marks data)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var marksexist = await defaultContext.Mark.FindAsync(data.MId);
                if (marksexist != null)
                {
                    marksexist.Student = data.Student;
                    marksexist.Subject = data.Subject;
                    marksexist.MarkObtained = data.MarkObtained;
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "Marks not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating marks data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int id)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                Marks marksexist = defaultContext.Mark.AsNoTracking().FirstOrDefault(t => t.MId == id);
                if (marksexist != null)
                {
                    defaultContext.Mark.Remove(marksexist);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "Marks not found with MId";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in get Deleting marks"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
    }
}
