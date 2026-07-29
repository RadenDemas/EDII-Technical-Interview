using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EDIITechincalInterview.Models
{
    public class Biodata
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public IdentityUser User { get; set; } = null!;

        [Required]
        [Display(Name = "Posisi yang Dilamar")]
        [StringLength(100)]
        public string PosisiDilamar { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama Lengkap")]
        [StringLength(100)]
        public string Nama { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nomor KTP")]
        [StringLength(16)]
        public string NomorKtp { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tempat Lahir")]
        public string TempatLahir { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tanggal Lahir")]
        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }

        [Required]
        [Display(Name = "Jenis Kelamin")]
        public string JenisKelamin { get; set; } = string.Empty;

        [Required]
        public string Agama { get; set; } = string.Empty;

        [Display(Name = "Golongan Darah")]
        public string? GolonganDarah { get; set; }

        [Display(Name = "Status")]
        public string? Status { get; set; }

        [Required]
        [Display(Name = "Alamat KTP")]
        public string AlamatKtp { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Alamat Tinggal")]
        public string AlamatTinggal { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Nomor Telepon")]
        public string NomorTelepon { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Orang Terdekat yang Dapat Dihubungi")]
        public string KontakDarurat { get; set; } = string.Empty;

        [Display(Name = "Skill")]
        public string? Skill { get; set; }

        [Display(Name = "Bersedia Ditempatkan")]
        public bool BersediaDitempatkan { get; set; }

        [Display(Name = "Penghasilan yang Diharapkan")]
        [Range(0, double.MaxValue)]
        public decimal PenghasilanDiharapkan { get; set; }

        [Display(Name = "Tanda Tangan Digital")]
        public string? TandaTanganDigital { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<PendidikanTerakhir> PendidikanTerakhir { get; set; } = new List<PendidikanTerakhir>();

        public ICollection<RiwayatPelatihan> RiwayatPelatihan { get; set; } = new List<RiwayatPelatihan>();

        public ICollection<RiwayatPekerjaan> RiwayatPekerjaan { get; set; } = new List<RiwayatPekerjaan>();
    }
}