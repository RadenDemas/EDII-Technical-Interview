using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class RiwayatPekerjaanViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Perusahaan harus diisi")]
        [Display(Name = "Nama Perusahaan")]
        public string NamaPerusahaan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Posisi Terakhir harus diisi")]
        [Display(Name = "Posisi Terakhir")]
        public string PosisiTerakhir { get; set; } = string.Empty;

        [Display(Name = "Pendapatan Terakhir")]
        [Range(0, double.MaxValue, ErrorMessage = "Nilai pendapatan tidak valid")]
        public decimal PendapatanTerakhir { get; set; }

        [Required(ErrorMessage = "Tahun harus diisi")]
        public int Tahun { get; set; }
    }
}
