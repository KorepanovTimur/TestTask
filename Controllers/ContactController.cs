using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data.interfaces;
using PhoneBook.Data.models;
using PhoneBook.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    public class ContactController : Controller
    {
        //интерфейсы для связи с базой данных
        private IContact _contRep;
        private IPerson _persRep;

        public ContactController(IContact contrep, IPerson persrep)
        {
            _contRep = contrep;
            _persRep = persrep;
        }

        public ViewResult PersonContacts(int PersonID)
        {//заполняем модель для вывода данных о клиенте
            var contacts = new ContactViewModel { contacts = _contRep.AllContacts(PersonID), person = _persRep.GetPerson(PersonID) };
            return View(contacts);
        }

        public IActionResult DeletePerson(int PersonID)
        {//при удалении контакта, сначала удалим все его контакты из базы, а после этого все его контакты
            while (_contRep.AllContacts(PersonID).Count() != 0)
            {
                _contRep.DeleteContact(_contRep.AllContacts(PersonID).First());
            }
            _persRep.DeletePerson(_persRep.GetPerson(PersonID));
            return RedirectToAction("Index", "Home");
        }
    }
}