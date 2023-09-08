using Identity_Provider.Domain.Entities.Users;

namespace Identity_Provider.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, User>
{
    public Task<User?> GetByEmailAsync(string email);
}
