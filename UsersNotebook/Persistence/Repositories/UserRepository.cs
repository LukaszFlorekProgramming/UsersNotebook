using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using UsersNotebook.Core.Models;
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

        public AdditionalInformation GetAdditionalInformationsByUserId(int id)
        {
            var information = _context.AdditionalInformations.FirstOrDefault(u => u.UserId == id);
            return information;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void AddInformation(AdditionalInformation additionalInformation)
        {
            _context.AdditionalInformations.Add(additionalInformation);
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

        public void UpdateInformation(AdditionalInformation additionalInformation)
        {
            var additionalInformationToUpdate = _context.AdditionalInformations
                .FirstOrDefault(x => x.UserId == additionalInformation.UserId);

            if (additionalInformationToUpdate is null)
            {
                additionalInformationToUpdate = new AdditionalInformation
                {
                    UserId = additionalInformation.UserId
                };
                _context.Add(additionalInformationToUpdate);
            }

            additionalInformationToUpdate.InformationType = additionalInformation.InformationType ?? string.Empty;
            additionalInformationToUpdate.InformationValue = additionalInformation.InformationValue ?? string.Empty;

            _context.SaveChanges();
        }

        public IEnumerable<User> Get(FilterUsers filter)
        {
            var predicate = PredicateBuilder.New<User>(true);

            if (!string.IsNullOrEmpty(filter.Name))
                predicate.And(x => x.Name == filter.Name);

            if (!string.IsNullOrEmpty(filter.Surname))
                predicate.And(x => x.Surname == filter.Surname);

            if (!string.IsNullOrEmpty(filter.Gender))
                predicate.And(x => x.Gender == filter.Gender);

            var users = _context.Users
                               .AsNoTracking()
                               .Where(predicate)
                               .AsEnumerable();
            return users;
        }
    }
}
