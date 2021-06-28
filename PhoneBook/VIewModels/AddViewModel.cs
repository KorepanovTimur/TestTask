using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.VIewModels
{
    public class AddViewModel
    { //модель для добавления нового человека с контактами в базу
        public AddViewModel()
        {
            forAddContacts = new List<Contact> { };
        }
        public List<Contact> forAddContacts { get; set; }
        public Person person { get; set; }

    }
}
