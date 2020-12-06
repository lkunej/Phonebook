using PhoneBookNamespace.BindingModels;
using PhoneBookNamespace.DAL.DbInitialization;
using PhoneBookNamespace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PhoneBookNamespace.Services
{
    public class ContactsService
    {
        private PhonebookContext context;
        public ContactsService()
        {
            context = new PhonebookContext();
        }

        public List<Contact> GetContacts()
        {
            try
            {
                List<Contact> contacts = context.Contacts.ToList();

                return contacts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Contact GetContactById(Guid? id)
        {
            try
            {
                Contact contact = context.Contacts.Find(id);

                return contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EditContact(ContactBindingModel model)
        {
            try
            {
                Contact contact = context.Contacts.Include(x=> x.PhoneNumbers).Where(x => x.Id == model.Id).FirstOrDefault();
                Contact existingContact = null;

                if (contact.Name != model.Name || contact.Lastname != model.Lastname
                    || contact.Address != model.Address)
                {
                    existingContact = context.Contacts.Where(x => (x.Name == model.Name &&
                                                                 x.Lastname == model.Lastname &&
                                                                 x.Address == model.Address)).FirstOrDefault();
                }

                var removedNumbersIds = contact.PhoneNumbers.Where(p => p.Number != null && model.PhoneNumbers.All(p2 => p2.Number != p.Number)).Select(x => x.Id).ToList();

                if (existingContact == null)
                {
                    contact.Address = model.Address;
                    contact.DateOfBirth = model.DateOfBirth;
                    contact.Name = model.Name;
                    contact.Lastname = model.Lastname;
                    PhoneNumber tmp;
                    foreach(var num in model.PhoneNumbers.Where(x => !String.IsNullOrEmpty(x.Number)))
                    {
                        tmp = context.PhoneNumbers.Find(num.Id);
                        if(tmp != null)
                        {
                            tmp.Number = num.Number;
                        }
                        else
                        {
                            tmp = new PhoneNumber();
                            tmp.Id = Guid.NewGuid();
                            tmp.ContactId = model.Id.Value;
                            tmp.Number = num.Number;
                            context.PhoneNumbers.Add(tmp);
                        }
                    }

                    foreach(var id in removedNumbersIds)
                    {
                        PhoneNumber num = context.PhoneNumbers.Find(id);
                        if(num != null)context.PhoneNumbers.Remove(num);
                    }
                    
                    context.Entry(contact).State = EntityState.Modified;
                    context.SaveChanges();
                } else
                {
                    throw new Exception("Contact exists!");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteContact(Contact contact)
        {
            try
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddContact(ContactBindingModel model)
        {
            try
            {
                model.Id = Guid.NewGuid();               

                Contact existingContact = context.Contacts.Where(x => (x.Name == model.Name &&
                                                                 x.Lastname == model.Lastname &&
                                                                 x.Address == model.Address) ).FirstOrDefault();

                if(existingContact == null)
                {
                    Contact contact = new Contact(model);

                    foreach (var n in model.PhoneNumbers)
                    {
                        n.Id = Guid.NewGuid();
                        n.ContactId = model.Id.Value;
                    }

                    context.Contacts.Add(contact);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Contact exists!");
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ContactBindingModel GetPhoneNunbersForContact(Contact contact)
        {
            try
            {
                ContactBindingModel res = new ContactBindingModel();

                List<PhoneNumber> phoneNumbers = context.PhoneNumbers.Where(x => x.ContactId == contact.Id).ToList();
                
                res.Name = contact.Name;
                res.Lastname = contact.Lastname;
                res.Id = contact.Id;
                res.PhoneNumbers = phoneNumbers;

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}