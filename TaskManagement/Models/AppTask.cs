using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Performers { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Status Status { get; set; }
        public TimeSpan ScheduledExecutionTime { get; set; }
        public TimeSpan? ActualExecutionTime { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
