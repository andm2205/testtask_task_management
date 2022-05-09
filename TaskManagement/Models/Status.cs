using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Controllers;

namespace TaskManagement.Models
{
    public enum Status
    {
        [Display(Name = "Assigned")]
        Assigned,
        [Display(Name = "InProgress")]
        InProgress,
        [Display(Name = "Suspended")]
        Suspended,
        [Display(Name = "Completed")]
        Completed
    }
}
