using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public int LastName { get; set; }
        public int Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Person()
        {
            Messages = new List<Message>();
        }
    }
}
