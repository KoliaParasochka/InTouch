using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InTouch.Models
{
    /// <summary>
    /// This class was created to demonstrate work with automapper
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}