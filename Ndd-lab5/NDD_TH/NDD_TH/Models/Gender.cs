using System.ComponentModel.DataAnnotations;

namespace NDD_TH.Models;

public enum Gender
{
    [Display(Name = "Nam")]
    Male = 1,
    [Display(Name = "Nữ")]
    Female = 2
}
