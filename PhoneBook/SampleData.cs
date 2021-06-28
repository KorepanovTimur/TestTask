using PhoneBook.Data;
using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class SampleData
    {
        public static void Initialize(AppDBContent context)
        {

            if (!context.Person.Any())
            {
                context.Person.AddRange(
                    new Person
                    {
                        Name = "Иван",
                        Surname = "Иванов",
                        Organization = "ЦВТ",

                    },
                    new Person
                    {
                        Name = "Григорий",
                        Surname = "Григоров",
                        Organization = "Ростелеком",

                    }
                );
                context.SaveChanges();
            }
        }
    }
}
