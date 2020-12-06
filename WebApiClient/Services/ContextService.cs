using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient.Data;
using WebApiClient.Interfaces;
using WebApiClient.Models;

namespace WebApiClient.Services
{
    public class ContextService:IContextService
    {
        PersonContext _context;

        public ContextService(PersonContext context)
        {
            _context = context;
        }

        public void AddPerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public List<Person> GetAllPerson()
        {
            return _context.People.ToList();
        }
    }
}
