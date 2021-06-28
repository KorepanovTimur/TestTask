using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data.interfaces;
using PhoneBook.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {   //Интерфейсы для связи с абзой данных
        private IPerson _persRep;
        private IContact _contRep;

        public HomeController(IPerson persrep, IContact contrep)
        {
            _persRep = persrep;
            _contRep = contrep;
        }
        //при обращении к корню приложения выводятся все контакты, при обращени при заполненом параметре search выводит найженые контакты
        [Route("/")]
        [Route("/{search}")]
        public ViewResult Index(string search)
        {
            ViewBag.IsSearched = false;
            HomeViewModel persons = null;
            //выводим все контакты
            if (string.IsNullOrEmpty(search)) 
            { 
            persons = new HomeViewModel { persons = _persRep.AllPersons };
                ViewBag.IsSearched = false;
                return View(persons);
            }
            //выводим контакты в соответствии с поиском
            else
            {
                //ищем во всех полях а так же по всем контактам
                var contacts = _contRep.AllContact.Where(c => c.ContactContent.Contains(search, StringComparison.OrdinalIgnoreCase));
                persons = new HomeViewModel
                {
                    persons = _persRep.AllPersons.Where(p =>
                    (p.Name != null && p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Surname != null && p.Surname.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Patronymic != null && p.Patronymic.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Organization != null && p.Organization.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Position != null &&  p.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (contacts.Any(c => p.Id==c.PersonId))
                    )
                };
                
                ViewBag.IsSearched = true;
               return View(persons);
            }
        }
    }
}
