using Microsoft.AspNetCore.Mvc;
using UsersNotebook.Persistence.Repositories;
using UsersNotebook.Persistence;
using UsersNotebook.Core.ViewModels;

namespace UsersNotebook.Controllers
{
    public class ReportController : Controller
    {
        private UserRepository _userRepository;

        public ReportController(UserDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public IActionResult UsersReport()
        {
            var vm = new UsersReportViewModel
            {
                FilterUsers = new Core.Models.FilterUsers(),
                Users = _userRepository.GetAll()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult UsersReport(UsersReportViewModel viewModel)
        {
            var users = _userRepository.Get(viewModel.FilterUsers.Name,
                viewModel.FilterUsers.Surname);

            return PartialView("_UsersReportTable", users);
        }
    }
}
