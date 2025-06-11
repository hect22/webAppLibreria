using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using webAppGallery.Models;
using webAppGallery.Services;

namespace webAppGallery.Controllers
{
    public class carController : Controller
    {
        public readonly carServices _carServices;
            public carController(carServices carServices)         
            {  
                   this._carServices = carServices;
            }


        // GET: carController
        public ActionResult Index()

        {
            var cars = _carServices.GetCars();
            return View(cars);
        }

        // GET: carController/Details/5
        public ActionResult Details(ObjectId id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var cars= _carServices.get(id);
            if (cars ==null)
            {
                return NotFound();
            }
            return View(cars);
            
        }

        // GET: carController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: carController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                _carServices.create(car);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: carController/Edit/5
        public ActionResult Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cars = _carServices.get(id);
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // POST: carController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ObjectId id, Car carId)
        {
            if(id !=carId.id)
            {
                return NotFound();
            
            }
            if(ModelState.IsValid)
            {
                _carServices.update(id, carId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(carId);
            }
        }

        // GET: carController/Delete/5
        public ActionResult Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cars = _carServices.get(id);
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // POST: carController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ObjectId id)
        {
            try
            {

                var cars= _carServices.get(id);
                if(cars == null)
                {

                    return NotFound();
                }
                _carServices.delete(cars.id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
