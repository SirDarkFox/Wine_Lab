using System.ComponentModel.DataAnnotations;

namespace Wine_Lab.ViewModels.Regulation
{
    public class RegulationViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Введите название регламента")]
        [StringLength(50, ErrorMessage = "Слишком длинное название регламента")]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
