using Dapper;
using Identity_Provider.DataAccess.Interfaces.Users;
using Identity_Provider.Domain.Entities.Users;

namespace Identity_Provider.DataAccess.Repositories.Users;

public class UserRepositoy : BaseRepository, IUserRepository
{
    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.users where identity_provider = @Email;";
            var data = await _connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<int> UpdateAsync(long id, User entity)
    {
        throw new NotImplementedException();
    }
}
