namespace LMSystem.Models
{
    public class DashboardModel
    {
        public int TotalStudents { get; set; }
        public int TotalBooks { get; set; }
        public int TotalLibrarians { get; set; }
        public int TotalBorrowings { get; set; }
        public int TotalPublications { get; set; }  // for newspapers + magazines
    }
}