using ProjectDb.Entities;
using ProjectDb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InTouch.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        EFUnitOfWork repository;

        public UserController()
        {
            repository = new EFUnitOfWork("DefaultConnection");
        }


        // GET: User
        public ActionResult Index()
        {
            Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
            if (person != null)
            {
                ViewBag.Messages = person.Messages;
                return View();
            }
            else
            {
                return new HttpNotFoundResult();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
                person.Messages.Add(message);
                repository.People.Update(person);
            }
            return RedirectToAction("Index");
        }
    }
}