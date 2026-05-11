// Student.cs
namespace LabyonetimSistemi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentNumber { get; set; } // Öğrenci no (login adı olacak)
        public int Grade { get; set; }
        public int ComputerId { get; set; }
        public int? UserId { get; set; } // Otomatik oluşturulan hesap
    }
}
