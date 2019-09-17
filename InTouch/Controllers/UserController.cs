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
        private int idMessage { get; set; }
        public UserController()
        {
            repository = new EFUnitOfWork("DefaultConnection");
        }

        /// <summary>
        /// This is the which works with main user page
        /// </summary>
        /// <returns></returns>
        // GET: User
        public ActionResult Index()
        {
            Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
            int id = person.Messages.ToList().Last().Id;
            if (person != null)
            {
                ViewBag.Time = DateTime.Now;
                ViewBag.Messages = person.Messages;
                return View();
            }
            else
            {
                return new HttpNotFoundResult();
            }

        }

        /// <summary>
        /// This action sends message to server
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
                message.TimeToDelete = DateTime.Now.AddMinutes(15);
                person.Messages.Add(message);
                repository.People.Update(person);
                
            }
            
            return RedirectToAction("Index");
        }


        /// <summary>
        /// This action deletes message from server
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMessage(int id)
        {
            repository.Messages.Delete(id);
            return RedirectToAction("Index");
        }

    }
}