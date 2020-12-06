using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIApp.Models;
using APIApp.Models.DOTModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClient.Services;

namespace WebApiClient.Controllers
{
    public class ServiceController : Controller
    {
        ElementService<Service, DTOService> _service = new ElementService<Service, DTOService>();

        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await _service.GetElementById(id));
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Service element)
        {
            if (element.Id == 0)
            {
                var lsit = await _service.GetAll();
                var maxId = lsit.ToList().Max(x => x.Id);
                element.Id = maxId + 1;
            }
            _service.AddElement(element);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            return View(await _service.GetElementById(id));
        }


        [HttpPost]

        public ActionResult Edit(int id, Service element)
        {
            _service.PutElement(id, element);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _service.DeleteElement(id);
            return RedirectToAction("Index");
        }


    }
}
