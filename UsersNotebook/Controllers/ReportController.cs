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
        public IActionResult UsersReport(FilterUsers filter)
        {
            var users = _userRepository.Get(filter);

            return PartialView("_UsersReportTable", users);
        }
        [HttpGet]
        public IActionResult DownloadCsv(FilterUsers filter)
        {
            var users = _userRepository.Get(filter);
            var csvData = new StringBuilder();
            csvData.AppendLine("Tytuł;Imie;Nazwisko;Data urodzenia;Ilosc lat;Płeć");
            string title;
            DateTime today = DateTime.Today;
            int age;
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

    }
}
