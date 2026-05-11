namespace LabyonetimSistemi.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; } // Çözüldü mü?
        public int ComputerId { get; set; }
    }
}
