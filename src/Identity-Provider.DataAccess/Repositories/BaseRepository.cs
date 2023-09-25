using Npgsql;
namespace Identity_Provider.DataAccess.Repositories;

public class BaseRepository
{
    protected NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=Identity-Provider-db; User Id=postgres; Password=1234;");
    }
}
