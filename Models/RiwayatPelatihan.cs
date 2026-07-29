using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Models
{
    public class RiwayatPelatihan
    {
        public int Id { get; set; }

        [Required]
        public int BiodataId { get; set; }

        public Biodata Biodata { get; set; } = null!;

        [Required]
        [Display(Name = "Nama Kursus/Seminar")]
        [StringLength(150)]
        public string NamaKursusSeminar { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Sertifikat")]
        public bool MemilikiSertifikat { get; set; }

        [Required]
        [Display(Name = "Tahun")]
        public int Tahun { get; set; }
    }
}