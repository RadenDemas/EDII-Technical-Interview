using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EDIITechincalInterview.Models
{
    public class PendidikanTerakhir
    {
        public int Id { get; set; }

        [Required]
        public int BiodataId { get; set; }

        public Biodata Biodata { get; set; } = null!;

        [Required]
        [Display(Name = "Jenjang Pendidikan Terakhir")]
        [StringLength(50)]
        public string JenjangPendidikan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama Institusi Akademik")]
        [StringLength(150)]
        public string NamaInstitusi { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Jurusan")]
        [StringLength(100)]
        public string Jurusan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tahun Lulus")]
        public int TahunLulus { get; set; }

        [Display(Name = "IPK")]
        [Range(0.00, 4.00)]
        [Precision(3, 2)]
        public decimal? IPK { get; set; }
    }
}