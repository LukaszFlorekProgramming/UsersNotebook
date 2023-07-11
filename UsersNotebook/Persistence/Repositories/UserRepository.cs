using System.Collections;
using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Persistence.Repositories
{
    public class UserRepository
    {
        private UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public IEnumerable<AdditionalInformation> GetAdditionalInformation()
        {
            return _context.AdditionalInformations.ToList();
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users;
            return users.ToList();
        }

        public User Get(int id)
        {
            var user = _context.Users.Single(u => u.Id == id);
            return user;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            var userToUpdate = _context.Users.FirstOrDefault(x =>  x.Id == user.Id);

            userToUpdate.Name = user.Name;
            userToUpdate.Surname = user.Surname;
            userToUpdate.DateOfBirth = user.DateOfBirth;
            userToUpdate.Gender = user.Gender;

            _context.SaveChanges();
        }
    }
}
