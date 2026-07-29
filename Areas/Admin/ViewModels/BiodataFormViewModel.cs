using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class BiodataFormViewModel
    {
        [Required]
        [Display(Name = "Posisi yang Dilamar")]
        public string PosisiDilamar { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        [StringLength(16, MinimumLength = 16)]
        [Display(Name = "Nomor KTP")]
        public string NomorKtp { get; set; } = string.Empty;

        [Required]
        public string TempatLahir { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Lahir")]
        public DateTime TanggalLahir { get; set; }

        [Required]
        public string JenisKelamin { get; set; } = string.Empty;

        [Required]
        public string Agama { get; set; } = string.Empty;

        public string? GolonganDarah { get; set; }

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
        public string OrangTerdekat { get; set; } = string.Empty;

        public string? Skill { get; set; }

        [Display(Name = "Bersedia Ditempatkan di Seluruh Kantor Perusahaan")]
        public bool BersediaDitempatkan { get; set; }

        [Display(Name = "Penghasilan yang Diharapkan")]
        [Range(0, double.MaxValue)]
        public decimal PenghasilanDiharapkan { get; set; }

        public List<PendidikanTerakhirViewModel> Pendidikan { get; set; } = new();
        public List<RiwayatPelatihanViewModel> RiwayatPelatihan { get; set; } = new();
        public List<RiwayatPekerjaanViewModel> RiwayatPekerjaan { get; set; } = new();
    }
}
