using System.Collections;
using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Core.ViewModels
{
    public class UsersViewModel
    {
        public IList<User> Users { get; set; }
        public IList<AdditionalInformation> AdditionalInformations { get; set; }
    }
}
