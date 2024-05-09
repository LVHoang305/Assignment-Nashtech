using ASPNETCoreAPI_day2.Models;

namespace ASPNETCoreAPI_day2.Services
{
    public interface IPersonService
    {
        void Add(ViewPerson person);
        void Update(int id, ViewPerson person);
        void Delete(int id);
        List<Person> Filter(string? name, string? gender, string? birthPlace);
        List<Person> ListAllPeople();
    }
}
