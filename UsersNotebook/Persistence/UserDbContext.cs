using Microsoft.EntityFrameworkCore;
using UsersNotebook.Core.Models.Domains;

namespace UsersNotebook.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<AdditionalInformation> AdditionalInformations { get; set; }

}
