using StudentManagementSystem.Model;

namespace StudentManagementSystem.Process
{
    public class GlobalVeriable : IDisposable
    {
        public User CurrentUser { get; set; }
        public void Dispose() { GC.SuppressFinalize(this); }
    }
}
