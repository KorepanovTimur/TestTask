using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data.models
{//модель задающая поля для человека в списке контактов
    public class Person
    {
        //Id человка
        public int Id { get; set; }
        //Фамилия
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        //Имя
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        public string Name { get; set; }
        //Отчество
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        //Дата рождения
        [Display(Name = "Дата Рождения")]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
        //Название организации
        [Display(Name = "Название органмзации")]
        public string Organization { get; set; }
        //Должность
        [Display(Name = "Должность")]
        public string Position { get; set; }
    }
}
