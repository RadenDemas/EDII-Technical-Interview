using EDIITechincalInterview.Areas.Admin.ViewModels;
using EDIITechincalInterview.Constants;
using EDIITechincalInterview.Data;
using EDIITechincalInterview.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EDIITechincalInterview.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class BiodataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BiodataController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? searchNama, string? searchPosisi, string? searchPendidikan)
        {
            var query = _context.Biodatas
                .Include(b => b.User)
                .Include(b => b.PendidikanTerakhir)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchNama))
            {
                query = query.Where(b => b.Nama.Contains(searchNama));
            }

            if (!string.IsNullOrEmpty(searchPosisi))
            {
                query = query.Where(b => b.PosisiDilamar.Contains(searchPosisi));
            }

            if (!string.IsNullOrEmpty(searchPendidikan))
            {
                query = query.Where(b => b.PendidikanTerakhir.Any(p => p.JenjangPendidikan.Contains(searchPendidikan)));
            }

            var biodatas = await query.ToListAsync();

            var model = biodatas.Select(b => new BiodataIndexItemViewModel
            {
                Id = b.Id,
                Nama = b.Nama,
                PosisiDilamar = b.PosisiDilamar,
                Email = b.User?.Email ?? b.Email,
                PendidikanTerakhir = b.PendidikanTerakhir.OrderByDescending(p => p.TahunLulus).FirstOrDefault()?.JenjangPendidikan ?? "-"
            }).ToList();

            ViewData["searchNama"] = searchNama;
            ViewData["searchPosisi"] = searchPosisi;
            ViewData["searchPendidikan"] = searchPendidikan;

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var biodata = await _context.Biodatas
                .Include(b => b.User)
                .Include(b => b.PendidikanTerakhir)
                .Include(b => b.RiwayatPelatihan)
                .Include(b => b.RiwayatPekerjaan)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (biodata == null)
                return NotFound();

            var model = new BiodataDetailViewModel
            {
                BiodataId = biodata.Id,
                UserId = biodata.UserId,
                UserEmail = biodata.User?.Email,
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
        public async Task<IActionResult> Create()
        {
            var usersWithoutBiodata = await _userManager.Users
                .Where(u => !_context.Biodatas.Any(b => b.UserId == u.Id))
                .ToListAsync();

            var model = new BiodataCreateViewModel
            {
                UsersList = new SelectList(usersWithoutBiodata, "Id", "Email")
            };

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
            {
                var usersWithoutBiodata = await _userManager.Users
                    .Where(u => !_context.Biodatas.Any(b => b.UserId == u.Id))
                    .ToListAsync();
                model.UsersList = new SelectList(usersWithoutBiodata, "Id", "Email", model.UserId);
                return View(model);
            }

            var biodata = new Biodata
            {
                UserId = model.UserId,
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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var biodata = await _context.Biodatas
                .Include(b => b.User)
                .Include(b => b.PendidikanTerakhir)
                .Include(b => b.RiwayatPelatihan)
                .Include(b => b.RiwayatPekerjaan)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (biodata == null)
                return NotFound();

            var model = new BiodataEditViewModel
            {
                BiodataId = biodata.Id,
                UserId = biodata.UserId,
                UserEmail = biodata.User?.Email,
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

            if (!model.Pendidikan.Any()) model.Pendidikan.Add(new PendidikanTerakhirViewModel());
            if (!model.RiwayatPelatihan.Any()) model.RiwayatPelatihan.Add(new RiwayatPelatihanViewModel());
            if (!model.RiwayatPekerjaan.Any()) model.RiwayatPekerjaan.Add(new RiwayatPekerjaanViewModel());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BiodataEditViewModel model)
        {
            if (id != model.BiodataId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                model.UserEmail = user?.Email;
                return View(model);
            }

            var biodata = await _context.Biodatas
                .Include(b => b.PendidikanTerakhir)
                .Include(b => b.RiwayatPelatihan)
                .Include(b => b.RiwayatPekerjaan)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (biodata == null)
                return NotFound();

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

            _context.PendidikanTerakhirs.RemoveRange(biodata.PendidikanTerakhir);
            _context.RiwayatPelatihans.RemoveRange(biodata.RiwayatPelatihan);
            _context.RiwayatPekerjaans.RemoveRange(biodata.RiwayatPekerjaan);

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

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var biodata = await _context.Biodatas.FindAsync(id);
            if (biodata != null)
            {
                _context.Biodatas.Remove(biodata);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
