namespace TaskManagement.Models
{
    public enum Status
    {
        Assigned,
        InProgress,
        Suspended,
        Completed
    }
    public class AppTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Performers { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Status Status { get; set; }
        public TimeSpan ScheduledExecutionTime { get; set; }
        public TimeSpan? ActualExecutionTime { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? ParentId { get; set; }
        public AppTask? Parent { get; set; }
    }
    public class FullAppTask : AppTask
    {
        public List<FullAppTask> Children { get; set; } = new List<FullAppTask>();
    }
}
