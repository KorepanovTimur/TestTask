using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.VIewModels
{ //модель для представления, которая выводит информацию о человеке
    public class ContactViewModel
    {
        public IEnumerable<Contact> contacts { get; set; }
        public Person person { get; set; }
    }
}
