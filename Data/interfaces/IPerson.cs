using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data.interfaces
{
     public interface IPerson
     {
        IEnumerable<Person> AllPersons { get; }

        Person GetPerson(int personid);

        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);

     }
}
