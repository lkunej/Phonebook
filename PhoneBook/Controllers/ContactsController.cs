using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBookNamespace.Services;
using PhoneBookNamespace.DAL.DbInitialization;
using PhoneBookNamespace.DAL.Models;
using PhoneBookNamespace.BindingModels;
using Microsoft.AspNet.SignalR;
using PhoneBookNamespace.Hubs;

namespace PhoneBookNamespace.Controllers
{
    public class ContactsController : Controller
    {
        private ContactsService service;
        private IHubContext signalRContext;

        public ContactsController()
        {
            service = new ContactsService();
            signalRContext = GlobalHost.ConnectionManager.GetHubContext<PhonebookSignalRHub>();
        }

        // GET: Contacts
        public void Index()
        {
            ;
        }

        // GET: Contacts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = service.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create(Guid? phonebookId)
        {
            Uri url = new Uri(Request.Url.AbsoluteUri);

            ContactBindingModel contact = new ContactBindingModel();
            contact.PhonebookId = Guid.Parse(url.Segments.Last());
            return View(contact);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Lastname,DateOfBirth,Address,PhoneNumbers")] ContactBindingModel contact)
        {
            if (ModelState.IsValid)
            {
                Uri url = new Uri(Request.Url.AbsoluteUri);                
                contact.PhonebookId = Guid.Parse(url.Segments.Last());

                try
                {
                    service.AddContact(contact);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    return View(contact);
                }

                // Send a notification to frontend
                signalRContext.Clients.All.showNotification("Created contact: " + contact.Name + " "+contact.Lastname);

                return RedirectToAction("Details", "Phonebooks", new { id = contact.PhonebookId });
                
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact =service.GetContactById(id);
            ContactBindingModel model = new ContactBindingModel(contact);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhonebookId,Name,Lastname,DateOfBirth,Address,PhoneNumbers")] ContactBindingModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.EditContact(contact);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    return View(contact);
                }
                

                // Send a notification to frontend
                signalRContext.Clients.All.showNotification("Edited contact: " + contact.Name + " " + contact.Lastname);

                return RedirectToAction("Details", "Phonebooks", new { id = contact.PhonebookId });
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = service.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Contact contact = service.GetContactById(id);
            
            if(contact != null)
            {
                service.DeleteContact(contact);
            }

            // Send a notification to frontend
            signalRContext.Clients.All.showNotification("Deleted contact: " + contact.Name + " " + contact.Lastname);

            return RedirectToAction("Details", "Phonebooks", new { id = contact.PhonebookId });
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
