using System.Collections.Generic;
using System.Threading.Tasks;
using AnotherWebProject.Models;

namespace AnotherWebProject.Persistence.Interfaces
{
    public interface IPersistence
    {
        Task<int> SaveAsync(Person person);

        IEnumerable<Person> GetEntries();

        Task<int> DeleteAsync(int id);
    }
}
