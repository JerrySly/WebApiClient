using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiClient.Interfaces
{
    interface IService<T,G>
    {
        Task<IEnumerable<G>> GetAll();
        Task<T> GetElementById(int id);

        void AddElement(T element);

        void DeleteElement(int id);

        void PutElement(int id, T element);

    }
}
