using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.User.ViewModels
{
    public class RiwayatPelatihanViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Kursus / Seminar")]
        public string NamaKursusSeminar { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Memiliki Sertifikat")]
        public bool MemilikiSertifikat { get; set; }

        [Required]
        public int Tahun { get; set; }
    }
}