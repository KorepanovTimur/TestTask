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
    public class AddController: Controller
    {
        //интерфейсы для связи с базой данных
        private IContact _contRep;
        private IPerson _persRep;

        public AddController(IContact contRep, IPerson persRep)
        {
            _contRep = contRep;
            _persRep = persRep;
        }
        [HttpGet]
        public IActionResult AddContact(int PersonID)
        {
            //Проверяем,пришёл контакт из базы на редактирование или создаётся новый
            if (PersonID == 0) return View(new AddViewModel());
            else 
            { 
            var viewModel = new AddViewModel
            {
                person = _persRep.GetPerson(PersonID),

            };
            viewModel.forAddContacts.AddRange(_contRep.AllContacts(PersonID));
            return View(viewModel);
            }
        }
        [HttpPost]
        public IActionResult AddContact(AddViewModel viewModel, string action, int index)
        {    //выбор действия, удаление контакта, добавление полей для разных типов контактов и сохранение
            switch (action)
            {
                case "Delete":
                    if (viewModel.forAddContacts[index].ContactId != 0) _contRep.DeleteContact(viewModel.forAddContacts[index]);
                    viewModel.forAddContacts.RemoveAt(index);
                    return View(viewModel);
                case "AddPhone":
                    viewModel.forAddContacts.Add(new Contact { ContactType = TypeContact.Phone });
                    return View(viewModel);
                case "AddEmail":
                    viewModel.forAddContacts.Add(new Contact { ContactType = TypeContact.Email });
                    return View(viewModel);
                case "AddSkype":
                    viewModel.forAddContacts.Add(new Contact { ContactType = TypeContact.Skype });
                    return View(viewModel);
                case "AddOther":
                    viewModel.forAddContacts.Add(new Contact { ContactType = TypeContact.Other });
                    return View(viewModel);
                default:
                    //оставим возможность оставить поле дата рождения пустым, но в случае его заполнения оно будет проверятся
                    if (viewModel.person.Birthday == new DateTime(0001, 01, 01, 0, 0, 0)) ModelState["person.Birthday"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                    if (ModelState.IsValid)
                    {//сохраняем: если контакт пришёл из базы, обнавляем его значения в базе, если контакт новый добавляем его в базу
                        if(viewModel.person.Id == 0) { 
                        _persRep.AddPerson(viewModel.person);
                        foreach (var obj in viewModel.forAddContacts)
                        {
                            obj.PersonId = _persRep.AllPersons.Last().Id;
                            _contRep.AddContact(obj);
                        }
                        return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            _persRep.UpdatePerson(viewModel.person);
                            foreach (var obj in viewModel.forAddContacts)
                            {
                                if (obj.ContactId == 0)
                                {
                                    obj.PersonId = viewModel.person.Id;
                                    _contRep.AddContact(obj);
                                }
                                else 
                                {
                                    obj.PersonId = viewModel.person.Id;
                                    _contRep.UpdateContact(obj); 
                                }
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else { 
                       //если в контактах есть ошибки выводит предупреждение
                        for (int i = 7; i < ModelState.Values.Count(); i++)
                            if (!(ModelState.Values.ElementAt(i).Errors.Count == 0)) ViewBag.Error = "Некоректные контакты";
                    }
                    
                    return View(viewModel);
            }

        }
    }
}
