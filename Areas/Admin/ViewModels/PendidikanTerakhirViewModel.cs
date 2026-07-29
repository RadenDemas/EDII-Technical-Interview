using System.ComponentModel.DataAnnotations;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class PendidikanTerakhirViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jenjang Pendidikan harus diisi")]
        [Display(Name = "Jenjang Pendidikan Terakhir")]
        public string JenjangPendidikan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nama Institusi harus diisi")]
        [Display(Name = "Nama Institusi Akademik")]
        public string NamaInstitusiAkademik { get; set; } = string.Empty;

        [Required(ErrorMessage = "Jurusan harus diisi")]
        public string Jurusan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tahun Lulus harus diisi")]
        [Display(Name = "Tahun Lulus")]
        public int TahunLulus { get; set; }

        public decimal IPK { get; set; }
    }
}
