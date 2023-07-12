using Microsoft.AspNetCore.Mvc;
using UsersNotebook.Persistence.Repositories;
using UsersNotebook.Persistence;
using UsersNotebook.Core.ViewModels;
using System.Text;

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
        [HttpGet]
        public IActionResult DownloadCsv(UsersReportViewModel viewModel)
        {
            var users = _userRepository.GetAll();
            /*var users = _userRepository.Get(viewModel.FilterUsers.Name,
                viewModel.FilterUsers.Surname);*/

            var csvData = new StringBuilder();
            csvData.AppendLine("Imie,Nazwisko,Data urodzenia, Płeć");

            foreach (var user in users)
            {
                csvData.AppendLine($"{user.Name}, {user.Surname}, {user.DateOfBirth}, {user.Gender}");
            }

            var csvBytes = Encoding.UTF8.GetBytes(csvData.ToString());
            var fileName = $"{DateTime.Now:yyyyMMddHHmmss}.csv";

            return File(csvBytes, "text/csv", fileName);
        }

        /*public IActionResult DownloadCsv(UsersReportViewModel viewModel)
        {
            var users = _userRepository.GetAll();
                viewModel.FilterUsers.Surname);

            var csvData = new StringBuilder();
            csvData.AppendLine("Imię,Nazwisko");

            foreach (var user in users)
            {
                csvData.AppendLine($"{user.Name},{user.Surname}");
            }

            var csvBytes = Encoding.UTF8.GetBytes(csvData.ToString());
            var fileName = $"{DateTime.Now:yyyyMMddHHmmss}.csv";

            return File(csvBytes, "text/csv", fileName);
        }*/

    }
}
