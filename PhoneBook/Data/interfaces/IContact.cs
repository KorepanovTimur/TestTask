using PhoneBook.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data.interfaces
{
    public interface IContact
    {
        IEnumerable<Contact> AllContacts(int personid);
        void AddContact(Contact contact);
        IEnumerable<Contact> AllContact { get; }
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}
