namespace EDIITechincalInterview.Areas.User.ViewModels
{
    public class BiodataDetailViewModel : BiodataFormViewModel
    {
        public int BiodataId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}