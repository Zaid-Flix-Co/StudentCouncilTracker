using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Base.UserCU;

public abstract class UserCuBase : IHaveCreatedDate
{
    [Display(Name = "Автор записи")]
    [MaxLength(500)]
    public string? CreatedUserName { get; set; }

    [Display(Name = "Автор изменения")]
    [MaxLength(500)]
    public string? UpdatedUserName { get; set; }

    [Display(Name = "Дата создания")]
    public DateTime? CreatedDate { get; set; }

    [Display(Name = "Дата изменения")]
    public DateTime? UpdatedDate { get; set; }
}