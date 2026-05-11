namespace LabyonetimSistemi.Models;


public class Lab
{
    public int Id { get; set; }
    public string Name { get; set; } // Örn: "Lab-1"
    public List<Computer> Computers { get; set; } = new();
}