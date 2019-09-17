using ProjectDb.EF;
using ProjectDb.Entities;
using ProjectDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectDb.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        ApplicationDbContext db;

        public MessageRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(Message item)
        {
            db.Messages.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Message message = db.Messages.Find(id);
            if (message != null)
                db.Messages.Remove(message);
            db.SaveChanges();
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return db.Messages.Include(m => m.Person).Where(predicate).ToList();
        }

        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return db.Messages.Include(m => m.Person).ToList();
        }

        public void Update(Message item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
