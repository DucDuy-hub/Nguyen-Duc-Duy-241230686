using System.ComponentModel.DataAnnotations;

namespace NetCoreNDDLAB5.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        public string Name { get; set; }
    }
}