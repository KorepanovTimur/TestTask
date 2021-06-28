using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.interfaces;
using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repository
{ //реализация методов для взаимодействия с базой данных
    public class ContactRepository : IContact
    {   //переменая для связи с базой данных
        private readonly AppDBContent appDBContent;


        public ContactRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        //добавление контакта в базу
        public void AddContact(Contact contact)
        {
            appDBContent.Contact.Add(contact);
            appDBContent.SaveChanges();
        }
        //метод возвращающий все контакты человека по его id
        public IEnumerable<Contact> AllContacts (int personid) => appDBContent.Contact.Where(c => c.PersonId == personid);
        //обновление данных контакта в базе
        public void UpdateContact(Contact contact)
        {
            appDBContent.Contact.Update(contact);
            appDBContent.SaveChanges();
        }
        //уудаление контакта из базы
        public void DeleteContact(Contact contact)
        {
            appDBContent.Contact.Remove(contact);
            appDBContent.SaveChanges();
        }
        //метод возвращающий все контакты в базе
        public IEnumerable<Contact> AllContact => appDBContent.Contact;
    }
}