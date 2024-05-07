using System;
using System.Collections.Generic;
using System.Linq;
using ASPNETCoreMVC.Models;

namespace ASPNETCoreMVC.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _people;

        public PersonService()
        {
            _people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le Viet", Gender = "Male", DateOfBirth = new DateTime(2002, 5, 30), PhoneNumber = "1234567890", BirthPlace = "Ha Noi", IsGraduated = true },
                new Person { Id = 2, FirstName = "Giang", LastName = "Le", Gender = "Female", DateOfBirth = new DateTime(2007, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
                new Person { Id = 3, FirstName = "Phu", LastName = "Le", Gender = "Male", DateOfBirth = new DateTime(2000, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
                new Person { Id = 4, FirstName = "Mao", LastName = "Xam", Gender = "Female", DateOfBirth = new DateTime(1997, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
                new Person { Id = 5, FirstName = "Hac", LastName = "Cau", Gender = "Male", DateOfBirth = new DateTime(1999, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
                new Person { Id = 6, FirstName = "Chau", LastName = "Minhh", Gender = "Female", DateOfBirth = new DateTime(2000, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
            };
        }
        

        public void Create(Person person)
        {
            person.Id = _people.Any() ? _people.Max(p => p.Id) + 1 : 1;
            _people.Add(person);
        }

        public void Update(Person person)
        {
            var existingPerson = _people.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.Gender = person.Gender;
                existingPerson.DateOfBirth = person.DateOfBirth;
                existingPerson.PhoneNumber = person.PhoneNumber;
                existingPerson.BirthPlace = person.BirthPlace;
                existingPerson.IsGraduated = person.IsGraduated;
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

        public Person GetPersonById(int id)
        {
            return _people.FirstOrDefault(p => p.Id == id);
        }

        public List<Person> ListAllPeople()
        {
            return _people;
        }
    }
}

