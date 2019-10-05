using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using AnotherWebProject.Models;
using AnotherWebProject.Persistence.Interfaces;
using Dapper;

namespace AnotherWebProject.Persistence
{
    public class Persistence : IPersistence
    {
        public async Task<int> SaveAsync(Person person)
        {
            using (var connection = new SQLiteConnection("Data Source=database.db"))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();

                parameters.Add("@firstname", person.Firstname);
                parameters.Add("@lastname", person.Lastname);

                return await connection.QuerySingleAsync<int>("INSERT INTO directory (firstname, lastname) VALUES(@firstname, @lastname); SELECT LAST_INSERT_ROWID();", parameters);
            }
        }
    }
}
