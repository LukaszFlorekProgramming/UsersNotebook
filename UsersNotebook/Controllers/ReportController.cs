using Microsoft.AspNetCore.Mvc;
using UsersNotebook.Persistence.Repositories;
using UsersNotebook.Persistence;
using UsersNotebook.Core.ViewModels;
using System.Text;
using UsersNotebook.Core.Models.Domains;
using UsersNotebook.Core.Models;

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
        public IActionResult UsersReport(FilterUsers viewModel)
        {
            var users = _userRepository.Get(viewModel.Name,
                viewModel.Surname,viewModel.Gender);

            return PartialView("_UsersReportTable", users);
        }
        [HttpGet]
        public IActionResult DownloadCsv(UsersReportViewModel viewModel)
        {
            var users = _userRepository.GetAll();
            /*var users = _userRepository.Get(viewModel.FilterUsers.Name,
                viewModel.FilterUsers.Surname);*/

            var csvData = new StringBuilder();
            csvData.AppendLine("Tytuł;Imie;Nazwisko;Data urodzenia;Ilosc lat;Płeć");
            string title;
            DateTime today = DateTime.Today;
            int age;//= today.Year - dateOfBirth.Year;
            foreach (var user in users)
            {
                age = today.Year - user.DateOfBirth.Year;
                title = user.Gender == "Mężczyzna" ? "Pan" : "Pani";
                csvData.AppendLine($"{title};{user.Name};{user.Surname};{user.DateOfBirth};{age};{user.Gender}");
            }

            var csvBytes = Encoding.GetEncoding("iso-8859-2").GetBytes(csvData.ToString());
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
