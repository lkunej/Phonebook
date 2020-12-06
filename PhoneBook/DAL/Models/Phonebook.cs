using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookNamespace.DAL.Models
{
    public class Phonebook
    {
        public Phonebook()
        {
            Contacts = new List<Contact>();
        }

        public Guid PhoneBookId { get; set; }
        public string PhonebookName { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

    }
}