using EDIITechincalInterview.Areas.User.ViewModels;
using EDIITechincalInterview.Data;
using EDIITechincalInterview.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDIITechincalInterview.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class BiodataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BiodataController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var biodata = await _context.Biodatas
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (biodata == null)
                return RedirectToAction(nameof(Create));

            return RedirectToAction(nameof(Detail));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);

            bool sudahAda = await _context.Biodatas
                .AnyAsync(x => x.UserId == userId);

            if (sudahAda)
                return RedirectToAction(nameof(Detail));

            var model = new BiodataCreateViewModel();

            model.Pendidikan.Add(new PendidikanTerakhirViewModel());
            model.RiwayatPelatihan.Add(new RiwayatPelatihanViewModel());
            model.RiwayatPekerjaan.Add(new RiwayatPekerjaanViewModel());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BiodataCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);

            var biodata = new Biodata
            {
                UserId = userId,
                PosisiDilamar = model.PosisiDilamar,
                Nama = model.Nama,
                NomorKtp = model.NomorKtp,
                TempatLahir = model.TempatLahir,
                TanggalLahir = model.TanggalLahir,
                JenisKelamin = model.JenisKelamin,
                Agama = model.Agama,
                GolonganDarah = model.GolonganDarah,
                Status = model.Status,
                AlamatKtp = model.AlamatKtp,
                AlamatTinggal = model.AlamatTinggal,
                Email = model.Email,
                NomorTelepon = model.NomorTelepon,
                KontakDarurat = model.OrangTerdekat,
                Skill = model.Skill,
                BersediaDitempatkan = model.BersediaDitempatkan,
                PenghasilanDiharapkan = model.PenghasilanDiharapkan,
                CreatedAt = DateTime.Now
            };

            if (model.Pendidikan != null)
            {
                foreach (var edu in model.Pendidikan)
                {
                    biodata.PendidikanTerakhir.Add(new PendidikanTerakhir
                    {
                        JenjangPendidikan = edu.JenjangPendidikan,
                        NamaInstitusi = edu.NamaInstitusiAkademik,
                        Jurusan = edu.Jurusan,
                        TahunLulus = edu.TahunLulus,
                        IPK = edu.IPK
                    });
                }
            }

            if (model.RiwayatPelatihan != null)
            {
                foreach (var pel in model.RiwayatPelatihan)
                {
                    biodata.RiwayatPelatihan.Add(new RiwayatPelatihan
                    {
                        NamaKursusSeminar = pel.NamaKursusSeminar,
                        MemilikiSertifikat = pel.MemilikiSertifikat,
                        Tahun = pel.Tahun
                    });
                }
            }

            if (model.RiwayatPekerjaan != null)
            {
                foreach (var pek in model.RiwayatPekerjaan)
                {
                    biodata.RiwayatPekerjaan.Add(new RiwayatPekerjaan
                    {
                        NamaPerusahaan = pek.NamaPerusahaan,
                        PosisiTerakhir = pek.PosisiTerakhir,
                        PendapatanTerakhir = pek.PendapatanTerakhir,
                        Tahun = pek.Tahun
                    });
                }
            }

            _context.Biodatas.Add(biodata);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detail));
        }

        public async Task<IActionResult> Detail()
        {
            var userId = _userManager.GetUserId(User);

            var biodata = await _context.Biodatas
                .Include(b => b.PendidikanTerakhir)
                .Include(b => b.RiwayatPelatihan)
                .Include(b => b.RiwayatPekerjaan)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (biodata == null)
                return RedirectToAction(nameof(Create));

            var model = new BiodataDetailViewModel
            {
                BiodataId = biodata.Id,
                UserId = biodata.UserId,
                PosisiDilamar = biodata.PosisiDilamar,
                Nama = biodata.Nama,
                NomorKtp = biodata.NomorKtp,
                TempatLahir = biodata.TempatLahir,
                TanggalLahir = biodata.TanggalLahir,
                JenisKelamin = biodata.JenisKelamin,
                Agama = biodata.Agama,
                GolonganDarah = biodata.GolonganDarah,
                Status = biodata.Status,
                AlamatKtp = biodata.AlamatKtp,
                AlamatTinggal = biodata.AlamatTinggal,
                Email = biodata.Email,
                NomorTelepon = biodata.NomorTelepon,
                OrangTerdekat = biodata.KontakDarurat,
                Skill = biodata.Skill,
                BersediaDitempatkan = biodata.BersediaDitempatkan,
                PenghasilanDiharapkan = biodata.PenghasilanDiharapkan,
                Pendidikan = biodata.PendidikanTerakhir.Select(p => new PendidikanTerakhirViewModel
                {
                    Id = p.Id,
                    JenjangPendidikan = p.JenjangPendidikan,
                    NamaInstitusiAkademik = p.NamaInstitusi,
                    Jurusan = p.Jurusan,
                    TahunLulus = p.TahunLulus,
                    IPK = p.IPK ?? 0
                }).ToList(),
                RiwayatPelatihan = biodata.RiwayatPelatihan.Select(p => new RiwayatPelatihanViewModel
                {
                    Id = p.Id,
                    NamaKursusSeminar = p.NamaKursusSeminar,
                    MemilikiSertifikat = p.MemilikiSertifikat,
                    Tahun = p.Tahun
                }).ToList(),
                RiwayatPekerjaan = biodata.RiwayatPekerjaan.Select(p => new RiwayatPekerjaanViewModel
                {
                    Id = p.Id,
                    NamaPerusahaan = p.NamaPerusahaan,
                    PosisiTerakhir = p.PosisiTerakhir,
                    PendapatanTerakhir = p.PendapatanTerakhir,
                    Tahun = p.Tahun
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(User);

            var biodata = await _context.Biodatas
                .Include(b => b.PendidikanTerakhir)
                .Include(b => b.RiwayatPelatihan)
                .Include(b => b.RiwayatPekerjaan)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (biodata == null)
                return RedirectToAction(nameof(Create));

            var model = new BiodataEditViewModel
            {
                BiodataId = biodata.Id,
                PosisiDilamar = biodata.PosisiDilamar,
                Nama = biodata.Nama,
                NomorKtp = biodata.NomorKtp,
                TempatLahir = biodata.TempatLahir,
                TanggalLahir = biodata.TanggalLahir,
                JenisKelamin = biodata.JenisKelamin,
                Agama = biodata.Agama,
                GolonganDarah = biodata.GolonganDarah,
                Status = biodata.Status,
                AlamatKtp = biodata.AlamatKtp,
                AlamatTinggal = biodata.AlamatTinggal,
                Email = biodata.Email,
                NomorTelepon = biodata.NomorTelepon,
                OrangTerdekat = biodata.KontakDarurat,
                Skill = biodata.Skill,
                BersediaDitempatkan = biodata.BersediaDitempatkan,
                PenghasilanDiharapkan = biodata.PenghasilanDiharapkan,
                Pendidikan = biodata.PendidikanTerakhir.Select(p => new PendidikanTerakhirViewModel
                {
                    Id = p.Id,
                    JenjangPendidikan = p.JenjangPendidikan,
                    NamaInstitusiAkademik = p.NamaInstitusi,
                    Jurusan = p.Jurusan,
                    TahunLulus = p.TahunLulus,
                    IPK = p.IPK ?? 0
                }).ToList(),
                RiwayatPelatihan = biodata.RiwayatPelatihan.Select(p => new RiwayatPelatihanViewModel
                {
                    Id = p.Id,
                    NamaKursusSeminar = p.NamaKursusSeminar,
                    MemilikiSertifikat = p.MemilikiSertifikat,
                    Tahun = p.Tahun
                }).ToList(),
                RiwayatPekerjaan = biodata.RiwayatPekerjaan.Select(p => new RiwayatPekerjaanViewModel
                {
                    Id = p.Id,
                    NamaPerusahaan = p.NamaPerusahaan,
                    PosisiTerakhir = p.PosisiTerakhir,
                    PendapatanTerakhir = p.PendapatanTerakhir,
                    Tahun = p.Tahun
                }).ToList()
            };

            // Pastikan setidaknya ada 1 form kosong jika tabelnya kosong
            if (!model.Pendidikan.Any()) model.Pendidikan.Add(new PendidikanTerakhirViewModel());
            if (!model.RiwayatPelatihan.Any()) model.RiwayatPelatihan.Add(new RiwayatPelatihanViewModel());
            if (!model.RiwayatPekerjaan.Any()) model.RiwayatPekerjaan.Add(new RiwayatPekerjaanViewModel());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BiodataEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);

            var biodata = await _context.Biodatas
                .Include(b => b.PendidikanTerakhir)
                .Include(b => b.RiwayatPelatihan)
                .Include(b => b.RiwayatPekerjaan)
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Id == model.BiodataId);

            if (biodata == null)
                return NotFound();

            // 1. Update Properti Utama
            biodata.PosisiDilamar = model.PosisiDilamar;
            biodata.Nama = model.Nama;
            biodata.NomorKtp = model.NomorKtp;
            biodata.TempatLahir = model.TempatLahir;
            biodata.TanggalLahir = model.TanggalLahir;
            biodata.JenisKelamin = model.JenisKelamin;
            biodata.Agama = model.Agama;
            biodata.GolonganDarah = model.GolonganDarah;
            biodata.Status = model.Status;
            biodata.AlamatKtp = model.AlamatKtp;
            biodata.AlamatTinggal = model.AlamatTinggal;
            biodata.Email = model.Email;
            biodata.NomorTelepon = model.NomorTelepon;
            biodata.KontakDarurat = model.OrangTerdekat;
            biodata.Skill = model.Skill;
            biodata.BersediaDitempatkan = model.BersediaDitempatkan;
            biodata.PenghasilanDiharapkan = model.PenghasilanDiharapkan;
            biodata.UpdatedAt = DateTime.Now;

            // 2. Hapus Collection Lama
            _context.PendidikanTerakhirs.RemoveRange(biodata.PendidikanTerakhir);
            _context.RiwayatPelatihans.RemoveRange(biodata.RiwayatPelatihan);
            _context.RiwayatPekerjaans.RemoveRange(biodata.RiwayatPekerjaan);

            // 3. Tambahkan Collection Baru
            if (model.Pendidikan != null)
            {
                foreach (var edu in model.Pendidikan)
                {
                    biodata.PendidikanTerakhir.Add(new PendidikanTerakhir
                    {
                        JenjangPendidikan = edu.JenjangPendidikan,
                        NamaInstitusi = edu.NamaInstitusiAkademik,
                        Jurusan = edu.Jurusan,
                        TahunLulus = edu.TahunLulus,
                        IPK = edu.IPK
                    });
                }
            }

            if (model.RiwayatPelatihan != null)
            {
                foreach (var pel in model.RiwayatPelatihan)
                {
                    biodata.RiwayatPelatihan.Add(new RiwayatPelatihan
                    {
                        NamaKursusSeminar = pel.NamaKursusSeminar,
                        MemilikiSertifikat = pel.MemilikiSertifikat,
                        Tahun = pel.Tahun
                    });
                }
            }

            if (model.RiwayatPekerjaan != null)
            {
                foreach (var pek in model.RiwayatPekerjaan)
                {
                    biodata.RiwayatPekerjaan.Add(new RiwayatPekerjaan
                    {
                        NamaPerusahaan = pek.NamaPerusahaan,
                        PosisiTerakhir = pek.PosisiTerakhir,
                        PendapatanTerakhir = pek.PendapatanTerakhir,
                        Tahun = pek.Tahun
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detail));
        }
    }
}