using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class BiodataCreateViewModel : BiodataFormViewModel
    {
        [Required(ErrorMessage = "Pilih User yang akan dibuatkan biodata.")]
        [Display(Name = "User (Email)")]
        public string UserId { get; set; } = string.Empty;

        public SelectList? UsersList { get; set; }
    }
}
