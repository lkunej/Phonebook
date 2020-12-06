using PhoneBookNamespace.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookNamespace.DAL.Models
{
    public class Contact
    {
        public Contact()
        {
            PhoneNumbers = new List<PhoneNumber>();
        }

        public Contact(ContactBindingModel model)
        {
            Id = model.Id.Value;
            PhonebookId = model.PhonebookId.Value;
            Name = model.Name;
            Lastname = model.Lastname;
            DateOfBirth = model.DateOfBirth;
            Address = model.Address;

            PhoneNumbers = model.PhoneNumbers;
        }

        public override bool Equals(Object obj)
        {
            if(obj == null ||!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Contact c = (Contact)obj;

                return (Name == c.Name) && (Lastname == c.Lastname) && (Address == c.Address);
            }
        }

        public Guid Id { get; set; }
        public Guid PhonebookId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers {get; set;}
    }
}