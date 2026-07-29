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

        public IActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(BiodataEditViewModel model)
        {
            return View(model);
        }
    }
}