namespace StudentManagementSystem.Providers
{
    public class CalculateGrad
    {
        public string Calculategrad(double AverageMarks)
        {
            if (AverageMarks == null)
            {
                return "No marks found";
            }
            if ((AverageMarks >= 90))
            {
                return "A";
            }
            else if (AverageMarks >= 70 && AverageMarks < 90)
            {
                return "B";
            }
            else if (AverageMarks >= 50 && AverageMarks < 70)
            {
                return "C";
            }
            else if (AverageMarks >= 33 && AverageMarks < 50)
            {
                return "D";
            }
            else
            {
                return "fail";
            }
        }
    }
}
