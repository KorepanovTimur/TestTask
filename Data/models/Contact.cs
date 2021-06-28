using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBook.Data.models
{
    public class NotAllowedAttribute : ValidationAttribute
    {
        //метод предназначеный для валидации контактка в зависимости от его типа
        public override bool IsValid(object value)
        {
            Regex reg;
            Contact contact = value as Contact;
            if (contact.ContactContent is null) return true;
            switch (contact.ContactType)
            {
                case TypeContact.Phone:
                    reg = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
                    return reg.IsMatch(contact.ContactContent);
                case TypeContact.Email:
                    reg = new Regex(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$");
                    return reg.IsMatch(contact.ContactContent);
                default: return true;
            }
        }
    }
    //перечисление для типа контакта
    public enum TypeContact
    {
        Phone,
        Email,
        Skype,
        Other
    }
    //модель задающая поля контакта
    [NotAllowedAttribute(ErrorMessage = "Некоректный ввод")]
    public class Contact
    {
        // ID контакта
        public int ContactId { get; set; }
        // Id человека, которому соответствует контакт
        [BindNever]
        public int PersonId { get; set; }
        // Тип контакта
        public TypeContact ContactType { get; set; }
        // Содержание контакта
        public string ContactContent { get; set; }
    }
}
