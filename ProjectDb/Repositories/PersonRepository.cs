using ProjectDb.EF;
using ProjectDb.Entities;
using ProjectDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectDb.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        ApplicationDbContext db;

        public PersonRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(Person item)
        {
            db.People.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Person person = db.People.Find(id);
            if (person != null)
                db.People.Remove(person);
            db.SaveChanges();
        }

        public IEnumerable<Person> Find(Func<Person, bool> predicate)
        {
            return db.People.Include(p => p.Messages).Where(predicate).ToList();
        }

        public Person Get(int id)
        {
            return db.People.Find(id);
        }

        public IEnumerable<Person> GetAll()
        {
            return db.People.Include(p => p.Messages).ToList();
        }

        public void Update(Person item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
