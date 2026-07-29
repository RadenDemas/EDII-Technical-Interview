using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class BiodataEditViewModel : BiodataFormViewModel
    {
        public int BiodataId { get; set; }
        
        [Display(Name = "User (Email)")]
        public string UserId { get; set; } = string.Empty;
        
        public string? UserEmail { get; set; }
    }
}
