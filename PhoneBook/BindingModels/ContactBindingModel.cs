using PhoneBookNamespace.DAL.Models;
using System;
using System.Collections.Generic;

namespace PhoneBookNamespace.BindingModels
{
    public class ContactBindingModel
    {
        public ContactBindingModel()
        {
            PhoneNumbers = new List<PhoneNumber> { new PhoneNumber() };
        }

        public ContactBindingModel(Contact contact)
        {
            Id = contact.Id;
            PhonebookId = contact.PhonebookId;
            Name = contact.Name;
            Lastname = contact.Lastname;
            Address = contact.Address;
            DateOfBirth = contact.DateOfBirth;
            PhoneNumbers = (List<PhoneNumber>) contact.PhoneNumbers;
        }

        public Guid? Id { get; set; }
        public Guid? PhonebookId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}