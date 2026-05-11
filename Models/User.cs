namespace LabyonetimSistemi.Models;


public class User
{
    public int Id { get; set; }
    public string Username { get; set; } // Giriş adı (Örn: öğrenci numarası veya 'admin')
    public string Password { get; set; } // Şifre
    public string Role { get; set; }     // "Admin" veya "Student"
}
