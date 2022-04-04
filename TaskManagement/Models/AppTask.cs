using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class AppTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Performers { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Status Status { get; set; }
        public uint ScheduledExecutionTime { get; set; }
        public uint? ActualExecutionTime { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
