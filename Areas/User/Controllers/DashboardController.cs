using EDIITechincalInterview.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EDIITechincalInterview.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
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
            var userId = _userManager.GetUserId(User);
            
            bool hasBiodata = await _context.Biodatas.AnyAsync(x => x.UserId == userId);
            
            ViewBag.HasBiodata = hasBiodata;

            return View();
        }
    }
}