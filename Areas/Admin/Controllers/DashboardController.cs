using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDIITechincalInterview.Data;
using EDIITechincalInterview.Areas.Admin.ViewModels;

namespace EDIITechincalInterview.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var model = new DashboardViewModel
            {
                TotalUsers = await _userManager.Users.CountAsync(),
                TotalBiodata = await _context.Biodatas.CountAsync(),
                BiodataHariIni = await _context.Biodatas.CountAsync(b => b.CreatedAt >= today),
                RecentBiodatas = await _context.Biodatas
                                    .Include(b => b.User)
                                    .OrderByDescending(b => b.CreatedAt)
                                    .Take(5)
                                    .ToListAsync()
            };

            return View(model);
        }
    }
}