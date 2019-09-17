using ProjectDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Person> People { get; }
        IRepository<Message> Messages { get; }
    }
}
