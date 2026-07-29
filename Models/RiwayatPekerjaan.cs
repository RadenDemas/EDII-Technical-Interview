using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Models
{
    public class RiwayatPekerjaan
    {
        public int Id { get; set; }

        [Required]
        public int BiodataId { get; set; }

        public Biodata Biodata { get; set; } = null!;

        [Required]
        [Display(Name = "Nama Perusahaan")]
        [StringLength(150)]
        public string NamaPerusahaan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Posisi Terakhir")]
        [StringLength(100)]
        public string PosisiTerakhir { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Pendapatan Terakhir")]
        [Range(0, double.MaxValue)]
        public decimal PendapatanTerakhir { get; set; }

        [Required]
        [Display(Name = "Tahun")]
        public int Tahun { get; set; }
    }
}