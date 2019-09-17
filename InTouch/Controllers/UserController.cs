using ProjectDb.Entities;
using ProjectDb.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (person != null)
            {
                ViewBag.Person = person;
                ViewBag.Time = DateTime.Now;
                ViewBag.Messages = person.Messages.OrderByDescending(p => p.Id);
                return View();
            }
            else
            {
                return new HttpNotFoundResult();
            }

        }


        /// <summary>
        /// This action sfends message to server
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.ImgPath = string.Empty;
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
            Message message = repository.Messages.Get(id);
            // Checking the path of file
            if (!string.IsNullOrEmpty(message.ImgPath))
            {   // If path is not empty program removes file
                FileInfo file = new FileInfo(Server.MapPath(message.ImgPath));
                file.Delete();
            }
            repository.Messages.Delete(id);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // Getting the name of file
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // Getting the full path
                string path = Server.MapPath("~/Files/" + fileName);
                
                // Saving file
                upload.SaveAs(path);
                Message message = new Message { Text = string.Empty, ImgPath = "~/Files/" + fileName, TimeToDelete = DateTime.Now.AddMinutes(15) };
                Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
                person.Messages.Add(message);
                repository.People.Update(person);
            }
            return RedirectToAction("Index");
        }

    }
}