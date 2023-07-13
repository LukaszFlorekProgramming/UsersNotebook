using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Core.ViewModels
{
    public class UserViewModel
    {
        public string Heading { get; set; }
        public User User { get; set; }
        public AdditionalInformation AdditionalInformations { get; set; }
    }
}
