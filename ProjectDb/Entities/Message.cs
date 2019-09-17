using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImgPath { get; set; }
        public DateTime TimeToDelete { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }

        public int SecondPersonId { get; set; }
    }
}
