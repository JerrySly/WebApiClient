using APIApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApiClient.Interfaces;
using System.Reflection;

namespace WebApiClient.Services
{
    public class ElementService<T,G> : IService<T,G> 
        where T:class
        where G:class
    {
        HttpClient client = new HttpClient();
        string name = Activator.CreateInstance<T>().GetType().Name;
        string apiPath { get{
                return $"https://localhost:44386/api/"+$"{name}/";
              
                }
        }
      
        public async  void AddElement(T element)
        {
            var elementString = new StringContent(JsonConvert.SerializeObject(element), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(apiPath+$"post{name}",elementString);
        }

        public async void DeleteElement(int id)
        {
            var result = await client.DeleteAsync(apiPath + $"delete{name}/{id}");
        }

        public async Task<IEnumerable<G>> GetAll()
        {
            var result = await client.GetAsync(apiPath + "getall");
            var jsonString = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<G>>(jsonString);
        }

        public async Task<T> GetElementById(int id)
        {
            var result = await client.GetAsync(apiPath + $"get{name}/{id}");
            var jsonString = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async void PutElement(int id, T element)
        {

            var elementString = new StringContent(JsonConvert.SerializeObject(element), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(apiPath + $"put{name}/{id}", elementString);
        }
    }
}
