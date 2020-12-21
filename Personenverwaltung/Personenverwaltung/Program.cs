using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Personenverwaltung
{
    class Program
    {
        static void Main(string[] args)
        {
            // Small hack to print objects
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            // REQ 1: Create database tables
            // https://entityframeworkcore.com/approach-code-first (Run both commands in "Paket-Manager-Console")
            // See many-to-many:
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns
            using (var context = new PersonContext())
            {
                var persons = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(@"data.json"));

                var attachedHobbies = new Dictionary<int, Hobby>();

                // REQ 2: We know reuse several hobbies - but we are only allowed to insert them once
                // Step 1: Iterate over all persons and hobbies
                // Step 2: Check, if the entry already exists in attachedHobbies
                //         If not - store in in the list and call context.Entry(hobby)
                //         EF is now aware of this entry - you cant attach an entry with the same id twice
                foreach (var person in persons)
                {
                    foreach (var hobby in person.Hobbies)
                    {
                        
                    }

                    // REQ 3: Replace the hobby instances from person.Hobbies with the attached ones
                    //        Hint: 
                    /*        var dir = new Dictionary<int, string>{{1,"a"},{2,"b"},{3,"c"}};
		                      var ids = new List<int>{1,2};
		                      var filterted = dir.Where(e => ids.Contains(e.Key)).Select(e => e.Value).ToList();	
                     */
                    
                }

                // REQ 4: Add all persons
               

                context.SaveChanges();
            }

            using (var context = new PersonContext())
            {
                // REQ 1: Load postal 1618
                // Hint: https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager
                

                Console.WriteLine(serializer.Serialize(person)); // REQ2 : Ausgabe in eine TXT File und abgeben
            }

            using (var context = new PersonContext())
            {
                // REQ 1: Load person with where name contains 'Tatum'         
               

                // REQ 2: Delete first hobby
               

                // REQ 3: Change hobby to "Listen music"
                

                context.SaveChanges();
            }

            using (var context = new PersonContext())
            {
                // REQ 1: Load person with where name contains 'Tatum'         
                

                Console.WriteLine(serializer.Serialize(person)); // REQ2 : put outout to "RESULT1.txt"
            }

            // *********************************************************************************************************************************************************************************
            // Now with Dapper
            // *********************************************************************************************************************************************************************************

            // REQ 1: Create schema.pgsql: dapper.addresses, dapper.persons, dapper.hobbies, dapper.persons_hobbies

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true; // if you use e.g. full_name with class property FullName

            using (var connection = new NpgsqlConnection("Server=localhost;Database=postgres;User Id=postgres;Password=postgres;"))
            {
                connection.Open();

                var persons = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(@"data.json"));

                // Warning: Not the fastest way - in production maybe: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy?view=dotnet-plat-ext-5.0

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var person in persons)
                    {
                        // REQ 2: Insert Into dapper.addresses, dapper.persons
                        // Hint: You want to use connection.Query<T> (see https://github.com/StackExchange/Dapper). You want to use INSERT INTO ... RETURNING ... (see https://www.postgresql.org/docs/9.5/sql-insert.html)
                       
                        foreach (var hobby in person.Hobbies)
                        {
                            // REQ 3: Insert Into dapper.hobbies, dapper.persons_hobbies
                            // Hint: for hoobies you want to use ON CONFLICT DO NOTHING (see https://www.postgresql.org/docs/9.5/sql-insert.html)
                            
                        }
                    }

                    transaction.Commit();
                }
            }

            using (var connection = new NpgsqlConnection("Server=localhost;Database=postgres;User Id=postgres;Password=postgres;"))
            {
                connection.Open();

                // REQ 1: Load postal 1618
                // Hint: Try to make it with one query. Look exactly what will be return - install DBeaver for example or PGAdmin. See https://riptutorial.com/dapper/example/1197/one-to-many-mapping or https://dapper-tutorial.net/result-multi-mapping
                var personMap = new Dictionary<int, Person>();

                var person = connection.Query<Person, Address, Hobby, Person>("SELECT ...",
                    (person, address, hobby) =>
                    {
                        

                        return personMap[person.Id];
                    }, splitOn: "id, id, id").Distinct().ToList();

                Console.WriteLine(serializer.Serialize(person)); // REQ2 : put outout to "RESULT2.txt"
            }

            using (var connection = new NpgsqlConnection("Server=localhost;Database=postgres;User Id=postgres;Password=postgres;"))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    // REQ 1: Remove first hobby from person where name contains 'Tatum'

                    // REQ 2: Change hobby from person where name contains 'Tatum' to "Listen music"

                    // REQ: Make a SELECT of all tables in one of your favourite tools (e.g. pgAdmin) and post result to RESULT3.txt

                    transaction.Commit();
                }
            }

            Console.WriteLine("Finished");

            Console.ReadKey();
        }
    }
}
