using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using UsersNotebook.Core.Models.Domains;
using UsersNotebook.Core.ViewModels;
using UsersNotebook.Persistence;
using UsersNotebook.Persistence.Repositories;

namespace UsersNotebook.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _userRepository;

        public UserController(UserDbContext context)
        {
            _userRepository = new UserRepository(context);
        }
        public IActionResult Users()
        {
            var users = _userRepository.GetAll();
            var additionalInformations = _userRepository.GetAdditionalInformation();

            var vm = new UsersViewModel
            {
                Users = users,
                AdditionalInformations = additionalInformations
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Users(UsersViewModel viewModel)
        {
            var users = _userRepository.GetAll();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult User(int id = 0)
        {
            var user = id == 0 ?
                new User { Id = 0, Name = string.Empty, Surname = string.Empty, DateOfBirth = DateTime.Today, Gender=string.Empty } : 
                _userRepository.Get(id);

            var vm = new UserViewModel
            {
                User = user,
                Heading = id == 0 ?
                "Dodawanie nowego urzytkownika" : "Edytowanie urzytkownika",
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult User(User user)
        {
            if (!ModelState.IsValid)
            {
                var vm = new UserViewModel
                {
                    User = user,
                    Heading = user.Id == 0 ?
                "Dodawanie nowego urzytkownika" : "Edytowanie urzytkownika"
                };
                return View("User", vm);
            }
            if(user.Id == 0) 
                _userRepository.Add(user);
            else
                _userRepository.Update(user);

            return RedirectToAction("Users");
        }

    }
}
