using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class AppTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Performers { get; set; } = String.Empty;
        public DateTime RegistrationDate { get; set; }
        public Status Status { get; set; }
        public uint ScheduledExecutionTime { get; set; }
        public uint? ActualExecutionTime { get; set; }
        public DateTime? CompletionDate { get; set; }
        public uint? ParentId { get; set; }
        public AppTask? Parent { get; set; }
        public ICollection<AppTask>? Children { get; set; }
    }
}
