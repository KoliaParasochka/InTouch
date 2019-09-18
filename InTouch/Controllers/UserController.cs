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

        List<Message> stickers;

        public UserController()
        {
            repository = new EFUnitOfWork("DefaultConnection");
            stickers = new List<Message>();
            stickers.Add(new Message { Text = "sticker", Path = "~/Content/Images/Stickers/anrgy.png", IsImg = true, TimeToDelete = DateTime.Now });
            stickers.Add(new Message { Text = "sticker", Path = "~/Content/Images/Stickers/cool.jpg", IsImg = true, TimeToDelete = DateTime.Now });
            stickers.Add(new Message { Text = "sticker", Path = "~/Content/Images/Stickers/greenSmile.jpg", IsImg = true, TimeToDelete = DateTime.Now });
            stickers.Add(new Message { Text = "sticker", Path = "~/Content/Images/Stickers/respect.jpg", IsImg = true, TimeToDelete = DateTime.Now });
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
                ViewBag.Stickers = stickers;
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
                message.Path = string.Empty;
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
            if (!string.IsNullOrEmpty(message.Path) && message.Text != "sticker")
            {   // If path is not empty program removes file
                FileInfo file = new FileInfo(Server.MapPath(message.Path));
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
                string path = string.Empty;
                string ext = Path.GetExtension(fileName);
                Message message;
                // Getting the full path
                if (ext.Equals(".jpg") || ext.Equals(".jpeg") || ext.Equals(".png"))
                {
                    path = Server.MapPath("~/Pictures/" + fileName);
                    message = new Message { Text = string.Empty, Path = "~/Pictures/" + fileName, TimeToDelete = DateTime.Now.AddMinutes(15), IsImg = true };

                }
                else
                {
                    path = Server.MapPath("~/Files/" + fileName);
                    message = new Message { Text = string.Empty, Path = "~/Files/" + fileName, TimeToDelete = DateTime.Now.AddMinutes(15), IsImg = false, FileName = fileName };

                }
                // Saving file
                upload.SaveAs(path);
                Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
                person.Messages.Add(message);
                repository.People.Update(person);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult DownloadFile(string filePath)
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            string ext = Path.GetExtension(fileName); // Getting extantion.
            
            string path = Server.MapPath(filePath); // Getting a full path.
            string type = "application/docx" + ext;
            return File(path, type, fileName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSticker(string path)
        {
            Message message = stickers.Where(el => el.Path == path).FirstOrDefault();
            message.TimeToDelete = DateTime.Now.AddMinutes(15);
            Person person = repository.People.Find(p => p.Email == User.Identity.Name).FirstOrDefault();
            person.Messages.Add(message);
            repository.People.Update(person);
            return RedirectToAction("Index");
        }

    }
}