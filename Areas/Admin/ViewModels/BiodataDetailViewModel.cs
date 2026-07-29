namespace EDIITechincalInterview.Areas.Admin.ViewModels
{
    public class BiodataDetailViewModel : BiodataFormViewModel
    {
        public int BiodataId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
    }
}
