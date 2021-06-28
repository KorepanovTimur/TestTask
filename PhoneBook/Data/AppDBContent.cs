using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data
{
    public class AppDBContent : DbContext
    {        
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Person> Person { get; set; }
        public AppDBContent(DbContextOptions<AppDBContent> options):base(options)
        {
            Database.EnsureCreated();
        }
        //база данных человек и их контактов


    }
}
