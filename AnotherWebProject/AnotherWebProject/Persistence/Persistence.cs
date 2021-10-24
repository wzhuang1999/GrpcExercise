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
        
        public IEnumerable<Person> GetEntries()
        {
            using (var connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.OpenAsync();
                return connection.Query<Person>("select * from directory");
            }

        }
        
        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new SQLiteConnection("Data Source=database.db"))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();

                parameters.Add("@id", id);

                return await connection.QuerySingleAsync<int>("DELETE FROM directory where id=@id", parameters);
            }
        }

    }
    
    
}
