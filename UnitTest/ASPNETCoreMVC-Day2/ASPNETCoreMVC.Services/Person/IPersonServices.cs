using ASPNETCoreMVC.Models;

namespace ASPNETCoreMVC.Services
{
    public interface IPersonService
    {
        void Create(Person person);
        void Update(Person person);
        void Delete(int id);
        Person GetPersonById(int id);
        List<Person> ListAllPeople();
    }
}

