using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient.Models;

namespace WebApiClient.Interfaces
{
    public interface IContextService
    {
        List<Person> GetAllPerson();
        void AddPerson(Person person);
    }
}
