using UsersNotebook.Core.Models;
using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Core.ViewModels
{
    public class UsersReportViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public FilterUsers FilterUsers { get; set; }
    }
}
