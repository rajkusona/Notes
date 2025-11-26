namespace Notes.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public Priority Priority { get; set; } = Priority.Low;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string GetPriorityColor()
        {
            return Priority switch
            {
                Priority.Low => "#90EE90",
                Priority.Medium => "#FFD700",
                Priority.High => "#FF6B6B",
                _ => "#FFFFFF"
            };
        }

        public string GetPriorityBadgeClass()
        {
            return Priority switch
            {
                Priority.Low => "badge-success",
                Priority.Medium => "badge-warning",
                Priority.High => "badge-danger",
                _ => "badge-secondary"
            };
        }
    }
}
