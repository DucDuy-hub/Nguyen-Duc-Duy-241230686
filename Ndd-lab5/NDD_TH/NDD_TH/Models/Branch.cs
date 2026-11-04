using System.ComponentModel.DataAnnotations;

namespace NDD_TH.Models;

public enum Branch
{
    [Display(Name = "Công nghệ thông tin")]
    IT = 1,
    [Display(Name = "Kinh tế")]
    BE = 2,
    [Display(Name = "Công trình")]
    CE = 3,
    [Display(Name = "Điện - Điện tử")]
    EE = 4
}
