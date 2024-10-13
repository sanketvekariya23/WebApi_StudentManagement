using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Model;
using StudentManagementSystem.Providers;

namespace StudentManagementSystem.Process
{
    public class StudentProcess : GlobalVeriable
    {
        private readonly CalculateGrad grad;
        public StudentProcess(CalculateGrad grad){ this.grad = grad; }
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                using DefaultContext defaultContext = new();
                var students = await defaultContext.Student.Include(s => s.Subjects).Include(m => m.Marks).Select(student => new
                {
                    StudentName = student.StudentName,
                    EnrollmentNo = student.EnrollmentNo,
                    Address = student.Address,
                    Subjects = student.Subjects.Select(s => new { SubjectName = s.SubjectName }).ToList(),
                    Marks = student.Marks.Select(m => new { MarkObtained = m.MarkObtained }).ToList(),
                    AverageMarks = student.Marks.Any() ? student.Marks.Average(m => m.MarkObtained) : 0
                }).ToListAsync();
                apiResponse.data = students;
            }
            catch (Exception ex){apiResponse.Message = "Something went wrong in getting Student data";apiResponse.Status = (Byte)StatusFlag.Failed;return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Student student)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var studentexist = defaultContext.Student.Find(student.EnrollmentNo);
                if (studentexist == null)
                { 
                    await defaultContext.Student.AddAsync(student);
                    await defaultContext.SaveChangesAsync();    
                }
                else
                {
                    apiResponse.Message = "Change enrollement number";
                    apiResponse.Status= (Byte)StatusFlag.Failed;
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Creating Student data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Update(Student student)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var studentexist = await defaultContext.Student.FindAsync(student.StudentId);
                if (studentexist != null)
                {
                    studentexist.StudentName = student.StudentName;
                    studentexist.EnrollmentNo = student.EnrollmentNo;
                    studentexist.Address = student.Address; 
                    studentexist.BirthDate = student.BirthDate;
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Message = "user not found";
                    apiResponse.Status=(Byte)StatusFlag.Failed;
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in Updating Student data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int studentid)
        {
            ApiResponse apiResponse = new ApiResponse { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                Student studentexist = defaultContext.Student.AsNoTracking().FirstOrDefault(t => t.StudentId == studentid);
                if (studentexist != null)
                {
                    defaultContext.Student.Remove(studentexist);
                    await defaultContext.SaveChangesAsync();
                }
                else
                {
                    apiResponse.Status = (Byte)StatusFlag.Failed;
                    apiResponse.Message = "user not found";
                }
            }
            catch (Exception ex) { apiResponse.Message = "Something went wrong in deleting Student data"; apiResponse.Status = (Byte)StatusFlag.Failed; return apiResponse; }
            return apiResponse;
        }
    }
}


//Select(s => new { AverageMarks = s.Marks.Average(m => (double?)m.MarkObtained) ?? 0 })