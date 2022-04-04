using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Controllers;

namespace TaskManagement.Models
{
    public enum Status
    {
        [Display(Name = "Назначена")]
        Assigned,
        [Display(Name = "Выполняется")]
        InProgress,
        [Display(Name = "Приостановлена")]
        Suspended,
        [Display(Name = "Завершена")]
        Completed
    }
}
