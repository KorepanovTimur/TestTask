using PhoneBook.Data.interfaces;
using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repository
{
    public class PersonRepository : IPerson
    {//реализация методов для взаимодействия с базой данных
        private readonly AppDBContent appDBContent;
        public PersonRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        //метод возвращающий всех людей в базе
        public IEnumerable<Person> AllPersons => appDBContent.Person;
        //метод возвращающий человка по его id
        public Person GetPerson(int personid) => appDBContent.Person.First(c => c.Id == personid);
        //обновление данных человка в базе
        public void UpdatePerson(Person person)
        {
            appDBContent.Person.Update(person);
            appDBContent.SaveChanges();
        }
        //добавление человека в базу
        public void AddPerson(Person person)
        {
            appDBContent.Person.Add(person);
            appDBContent.SaveChanges();
        }
        //удаление человека из базы
        public void DeletePerson(Person person)
        {
            appDBContent.Person.Remove(person);
            appDBContent.SaveChanges();
        }
    }

}