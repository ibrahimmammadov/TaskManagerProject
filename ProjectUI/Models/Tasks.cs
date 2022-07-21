namespace ProjectUI.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string? Tittle { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<UserApp> UserApps { get; set; }
    }
}
