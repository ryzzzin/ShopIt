using System.ComponentModel.DataAnnotations;

namespace ShopIt.Models.AdminModels
{
    public class CreateStatusRequest
    {
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
