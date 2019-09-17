using ProjectDb.EF;
using ProjectDb.Entities;
using ProjectDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        ApplicationDbContext db;
        private PersonRepository personRepository;
        private MessageRepository messageRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationDbContext(connectionString);
        }

        public IRepository<Person> People
        {
            get
            {
                if (personRepository == null)
                    personRepository = new PersonRepository(db);
                return personRepository;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepository == null)
                    messageRepository = new MessageRepository(db);
                return messageRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        db.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
