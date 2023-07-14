using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Core.ViewModels
{
    public class UserViewModel
    {
        public string Heading { get; set; }
        public User User { get; set; }
        public List<AdditionalInformation> AdditionalInformations { get; set; }
        public AdditionalInformation AdditionalInformation { get; set; }

        public UserViewModel()
        {
            AdditionalInformations = new List<AdditionalInformation>();
        }
    }
}
