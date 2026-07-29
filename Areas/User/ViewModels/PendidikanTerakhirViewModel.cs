using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.User.ViewModels
{
    public class PendidikanTerakhirViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Jenjang Pendidikan")]
        public string JenjangPendidikan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama Institusi Akademik")]
        public string NamaInstitusiAkademik { get; set; } = string.Empty;

        [Required]
        public string Jurusan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tahun Lulus")]
        public int TahunLulus { get; set; }

        [Required]
        [Range(0, 4)]
        public decimal IPK { get; set; }
    }
}