using Microsoft.Data.Sqlite;
using Waterful.Models;

namespace Waterful;

class DbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration configuration) => 
        _connectionString = configuration.GetConnectionString("ConnectionString");

    public void Execute(string query)
    {
        using var db = new SqliteConnection(_connectionString);
        db.Open();
        var command = db.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
    }

    public List<WaterModel> GetLogs(string query = @"SELECT * FROM logs")
    {
        using var db = new SqliteConnection(_connectionString);
        db.Open();
        var command = db.CreateCommand();
        command.CommandText = query;

        var reader = command.ExecuteReader();
        var logs = new List<WaterModel>();

        while (reader.Read())
            logs.Add(new WaterModel
            {
                Id = reader.GetInt32(0),
                Date = reader.GetDateTime(1),
                Quantity = reader.GetInt32(2),
            });

        return logs;
    }
}
