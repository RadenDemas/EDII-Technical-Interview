using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class RiwayatPelatihanViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Kursus / Seminar harus diisi")]
        [Display(Name = "Nama Kursus / Seminar")]
        public string NamaKursusSeminar { get; set; } = string.Empty;

        [Display(Name = "Sertifikat")]
        public bool MemilikiSertifikat { get; set; }

        [Required(ErrorMessage = "Tahun harus diisi")]
        public int Tahun { get; set; }
    }
}
