using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class AppTask
    {
        public AppTask() { }
        public AppTask(AppTask appTask)
        {
            this.Id = appTask.Id;
            this.Name = appTask.Name;
            this.Description = appTask.Description;
            this.Status = appTask.Status;
            this.Performers = appTask.Performers;
            this.RegistrationDate = appTask.RegistrationDate;
            this.ScheduledExecutionTime = appTask.ScheduledExecutionTime;
            this.ActualExecutionTime = appTask.ActualExecutionTime;
            this.CompletionDate = appTask.CompletionDate;
            this.ParentId = appTask.ParentId;
            this.Parent = appTask.Parent;
            this.Children = appTask.Children;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; } = String.Empty;
        [Display(Name = "Description")]
        public string Description { get; set; } = String.Empty;
        [Display(Name = "Performers")]
        public string Performers { get; set; } = String.Empty;
        [Display(Name = "RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Status")]
        public Status Status { get; set; }
        [Display(Name = "ScheduledExecutionTime")]
        public uint ScheduledExecutionTime { get; set; }
        [Display(Name = "ActualExecutionTime")]
        public uint? ActualExecutionTime { get; set; }
        [Display(Name = "CompletionDate")]
        public DateTime? CompletionDate { get; set; }
        public uint? ParentId { get; set; }
        public AppTask? Parent { get; set; }
        public ICollection<AppTask>? Children { get; set; }
    }
}
