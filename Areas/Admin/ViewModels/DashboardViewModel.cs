using EDIITechincalInterview.Models;

namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalBiodata { get; set; }
        public int BiodataHariIni { get; set; }
        
        public List<Biodata> RecentBiodatas { get; set; } = new List<Biodata>();
    }
}
