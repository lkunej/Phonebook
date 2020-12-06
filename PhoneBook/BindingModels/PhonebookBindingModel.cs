using PagedList;
using PhoneBookNamespace.DAL.Models;
using System;
using System.Collections.Generic;

namespace PhoneBookNamespace.BindingModels
{
    public class PhonebookBindingModel
    {
        public PhonebookBindingModel()
        {
            Contacts = new List<Contact>();
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
        public IPagedList<Contact> ContactsPagedList {get; set;}

    }
}