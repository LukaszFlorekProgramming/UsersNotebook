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
            
            var additionalInformationToUpdate = _context.AdditionalInformations.FirstOrDefault(x => x.UserId == additionalInformation.UserId);

            if(additionalInformationToUpdate is null)
            {
                additionalInformationToUpdate = new AdditionalInformation();
                additionalInformationToUpdate.InformationType = additionalInformation.InformationType;
                additionalInformationToUpdate.InformationValue = additionalInformation.InformationValue;
                additionalInformationToUpdate.UserId = additionalInformation.UserId;
                _context.Add(additionalInformationToUpdate);
            }
            else
            {
            additionalInformationToUpdate.InformationType = additionalInformation.InformationType ?? string.Empty;
            additionalInformationToUpdate.InformationValue = additionalInformation.InformationValue ?? string.Empty;
            }
            _context.SaveChanges();
        }

        public IEnumerable<User> Get(string name, string surname, string gender)
        {
            var users = _context.Users.AsEnumerable(); ;

            if(!string.IsNullOrWhiteSpace(name))
            {
                users = users.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(surname))
            {
                users = users.Where(x => x.Surname.Contains(surname));
            }
            if (!string.IsNullOrWhiteSpace(gender))
            {
                users = users.Where(x => x.Gender.Contains(gender));
            }

            return users.ToList();
        }
    }
}
