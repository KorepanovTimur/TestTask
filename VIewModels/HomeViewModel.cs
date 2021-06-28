using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.VIewModels
{
    public class HomeViewModel
    { //модель для представления выводящего списка людей в базе
        public IEnumerable<Person> persons { get; set; }
    }
}
