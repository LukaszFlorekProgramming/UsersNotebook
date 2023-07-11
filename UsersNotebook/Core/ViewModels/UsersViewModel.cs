using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Core.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<AdditionalInformation> AdditionalInformations { get; set; }
    }
}
