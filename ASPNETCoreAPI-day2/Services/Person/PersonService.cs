using ASPNETCoreAPI_day2.Models;

namespace ASPNETCoreAPI_day2.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _people;

        public PersonService()
        {
            _people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le Viet", Gender = "Male", DateOfBirth = new DateTime(2002, 5, 30), BirthPlace = "Ha Noi"},
                new Person { Id = 2, FirstName = "He", LastName = "Le", Gender = "Male", DateOfBirth = new DateTime(2001, 5, 30), BirthPlace = "Ha Noi"},
                new Person { Id = 3, FirstName = "Du", LastName = "Nguyen", Gender = "Male", DateOfBirth = new DateTime(2003, 5, 30), BirthPlace = "Ho Chi Minh"},
                new Person { Id = 4, FirstName = "Hoai", LastName = "To", Gender = "Male", DateOfBirth = new DateTime(2000, 5, 30), BirthPlace = "Da Nang"},
                new Person { Id = 5, FirstName = "Ngan", LastName = "Nguyen", Gender = "Female", DateOfBirth = new DateTime(1999, 5, 30), BirthPlace = "Tien Giang"},
                new Person { Id = 6, FirstName = "Ly", LastName = "Ly", Gender = "Female", DateOfBirth = new DateTime(1998, 5, 30), BirthPlace = "Vinh Phuc"},
                new Person { Id = 7, FirstName = "Giang", LastName = "Le Doan Huong", Gender = "Female", DateOfBirth = new DateTime(1990, 5, 30), BirthPlace = "Yen Bai"},
                new Person { Id = 8, FirstName = "My", LastName = "Doan", Gender = "Female", DateOfBirth = new DateTime(2023, 5, 30), BirthPlace = "Ha Giang"},
            };
        }

        public void Add(ViewPerson person)
        {
            int id = _people.Count > 0 ? _people.Max(p => p.Id) + 1 : 1;

            _people.Add(new Person
            {
                Id = id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = Convert.ToDateTime(person.DateOfBirth),
                BirthPlace = person.BirthPlace
            });
        }

        public void Update(int id, ViewPerson updatedPerson)
        {
            var existingPerson = _people.FirstOrDefault(p => p.Id == id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = updatedPerson.FirstName;
                existingPerson.LastName = updatedPerson.LastName;
                existingPerson.DateOfBirth = Convert.ToDateTime(updatedPerson.DateOfBirth);
                existingPerson.Gender = updatedPerson.Gender;
                existingPerson.BirthPlace = updatedPerson.BirthPlace;
            }
        }

        public void Delete(int id)
        {
            var personToDelete = _people.FirstOrDefault(p => p.Id == id);
            if (personToDelete != null)
            {
                _people.Remove(personToDelete);
            }
        }

        public List<Person> Filter(string? name, string? gender, string? birthPlace)
        {
            var filteredPeople = _people;

            if (!string.IsNullOrEmpty(name))
            {
                filteredPeople = filteredPeople.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(gender))
            {
                filteredPeople = filteredPeople.Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(birthPlace))
            {
                filteredPeople = filteredPeople.Where(p => p.BirthPlace.Equals(birthPlace, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return filteredPeople;
        }

        public List<Person> ListAllPeople()
        {
            return _people;
        }
    }
}
