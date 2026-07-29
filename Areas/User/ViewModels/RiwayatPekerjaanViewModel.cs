using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.User.ViewModels
{
    public class RiwayatPekerjaanViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Perusahaan")]
        public string NamaPerusahaan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Posisi Terakhir")]
        public string PosisiTerakhir { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Pendapatan Terakhir")]
        public decimal PendapatanTerakhir { get; set; }

        [Required]
        public int Tahun { get; set; }
    }
}