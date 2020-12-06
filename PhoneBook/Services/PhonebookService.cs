using PagedList;
using PhoneBookNamespace.BindingModels;
using PhoneBookNamespace.DAL.DbInitialization;
using PhoneBookNamespace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookNamespace.Services
{ 
    public class PhonebookService
    {
        private PhonebookContext context;
        public PhonebookService()
        {
            context = new PhonebookContext();
        }

        public List<Phonebook> GetPhonebooks()
        {
            try
            {
                List<Phonebook> phonebooks = context.PhoneBooks.ToList();

                return phonebooks;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Phonebook GetPhonebookById(Guid? id)
        {
            try
            {
                Phonebook phonebook = context.PhoneBooks.Find(id);

                return phonebook;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EditPhonebook(Phonebook phonebook)
        {
            try
            {
                context.Entry(phonebook).State = EntityState.Modified;
                context.SaveChanges();
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeletePhonebook(Phonebook phonebook)
        {
            try
            {
                context.PhoneBooks.Remove(phonebook);
                context.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddPhonebook(Phonebook phonebook)
        {
            try
            {
                phonebook.PhoneBookId = Guid.NewGuid();
                context.PhoneBooks.Add(phonebook);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PhonebookBindingModel GetContactsInPhonebook(Phonebook phonebook, int page, int numPerPage)
        {
            try
            {
                PhonebookBindingModel res = new PhonebookBindingModel();

                List<Contact> contacts = context.Contacts.Where(x => x.PhonebookId == phonebook.PhoneBookId).OrderBy(x => x.Name)  .ToList();

                IPagedList<Contact> onePageOfContacts = contacts.ToPagedList(page, numPerPage);
                
                res.ContactsPagedList = onePageOfContacts;
                res.Name = phonebook.PhonebookName;
                res.Id = phonebook.PhoneBookId;

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}