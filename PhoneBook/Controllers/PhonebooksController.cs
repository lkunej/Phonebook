using System;
using System.Net;
using System.Web.Mvc;
using PhoneBookNamespace.Services;
using PhoneBookNamespace.DAL.Models;
using PhoneBookNamespace.Hubs;
using Microsoft.AspNet.SignalR;

namespace PhoneBookNamespace.Controllers
{
    public class PhonebooksController : Controller
    {
        private PhonebookService service;
        private IHubContext signalRContext;

        public PhonebooksController()
        {
            service = new PhonebookService();
            signalRContext = GlobalHost.ConnectionManager.GetHubContext<PhonebookSignalRHub>();
        }
        // GET: Phonebooks
        public ActionResult Index()
        {
            var phonebooks = service.GetPhonebooks();            

            return View(phonebooks);
        }

        //GET: Phonebooks/Details/5
        public ActionResult Details(Guid? id, int page=1, int numPerPage=10)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonebook phonebook = service.GetPhonebookById(id);
            if (phonebook == null)
            {                
                return HttpNotFound();
            }

            var model = service.GetContactsInPhonebook(phonebook, page, numPerPage);
            ViewBag.OnePageOfContacts = model.ContactsPagedList;

            return View(model);
        }

        // GET: Phonebooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phonebooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhonebookName")] Phonebook phonebook)
        {
            if (ModelState.IsValid)
            {
                service.AddPhonebook(phonebook);
                
                // Send a notification to frontend
                signalRContext.Clients.All.showNotification("Added a new phonebook ("+phonebook.PhonebookName+")");

                return RedirectToAction("Index");
            }

            return View(phonebook);
        }

        // GET: Phonebooks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonebook phonebook = service.GetPhonebookById(id);
            if (phonebook == null)
            {
                return HttpNotFound();
            }
            return View(phonebook);
        }

        // POST: Phonebooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhoneBookId,PhonebookName")] Phonebook phonebook)
        {
            if (ModelState.IsValid)
            {
                service.EditPhonebook(phonebook);

                // Send a notification to frontend
                signalRContext.Clients.All.showNotification("Edited phonebook: "+phonebook.PhonebookName);

                return RedirectToAction("Index");
            }
            return View(phonebook);
        }

        // GET: Phonebooks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonebook phonebook = service.GetPhonebookById(id);
            if (phonebook == null)
            {
                return HttpNotFound();
            }
            return View(phonebook);
        }

        // POST: Phonebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Phonebook phonebook = service.GetPhonebookById(id);

            if(phonebook != null)
            {
                service.DeletePhonebook(phonebook);
                // Send a notification to frontend
                signalRContext.Clients.All.showNotification("Deleted phonebook: " + phonebook.PhonebookName);
            }            
            
            return RedirectToAction("Index");
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
