using PhoneBookNamespace.DAL.Models;
using System;
using System.Collections.Generic;

namespace PhoneBookNamespace.DAL.DbInitialization
{
    public class DatabaseInitialization : System.Data.Entity.CreateDatabaseIfNotExists<PhonebookContext>
    {
        
        private PhonebookContext context;
        protected override void Seed(PhonebookContext context)
        {
            this.context = context;

            var phonebookGuid = Guid.NewGuid();
            var phonebook = new Phonebook {PhoneBookId= phonebookGuid , PhonebookName = "Default Phonebook" };

            context.PhoneBooks.Add(phonebook);

            var firstContactGuid = Guid.NewGuid();
            var secondContactGuid = Guid.NewGuid();

            

            var contacts = new List<Contact>
            {
                new Contact{Id=firstContactGuid, PhonebookId=phonebookGuid ,Name= "Default", Lastname="Contact"},
                new Contact{Id=secondContactGuid, PhonebookId=phonebookGuid, Name = "Default2", Lastname="Contact2"}
            };

            // Create 21 contacts to show pagination
            Contact tmp;
            Guid tmpGuid;
            for (var i = 3; i < 24; i++)
            {
                tmpGuid = Guid.NewGuid();
                tmp = new Contact { Id = tmpGuid, PhonebookId = phonebookGuid, Name = "Default"+i.ToString(), Lastname = "Contact" + i.ToString() };
                contacts.Add(tmp);
            }

            contacts.ForEach(c => context.Contacts.Add(c));

            var numbers = new List<PhoneNumber>
            {
                new PhoneNumber{Id= Guid.NewGuid(),ContactId=firstContactGuid ,Number="000 000 0001"},
                new PhoneNumber{Id= Guid.NewGuid(),ContactId=secondContactGuid ,Number="000 000 0002"}
            };

            numbers.ForEach(n => context.PhoneNumbers.Add(n));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}