using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using webAppGallery.Models;
using webAppGallery.Services;

namespace webAppGallery.Controllers
{
    public class orderController : Controller
    {

        public readonly orderServices _orderServices;
        private readonly carServices _carServices;

        public orderController(orderServices orderServices, carServices carServices)
        {
            this._orderServices = orderServices;
            this._carServices = carServices;
        }
        // GET: orderController
        public ActionResult Index()
        {
            var orders = _orderServices.GetOrders();
            return View(orders);
        }

        // GET: orderController/Details/5
        public ActionResult Details(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orders = _orderServices.get(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);

        }

        // GET: orderController/Create
        public ActionResult Create()
        {
            var cars = _carServices.GetCars();
            ViewBag.Cars = new SelectList(cars,"id", "marca");
            return View();
        }

        // POST: orderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order car)
        {
            try
            {

                _orderServices.create(car);
                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: orderController/Edit/5
        public ActionResult Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _orderServices.get(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: orderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
public ActionResult Edit(ObjectId id, Order orderId)
        {
            if (id != orderId.id)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _orderServices.update(id, orderId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(orderId);
            }
        }

        // GET: orderController/Delete/5
        public ActionResult Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orders = _orderServices.get(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }


        // POST: orderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ObjectId id)
        {
            try
            {

                var orders = _orderServices.get(id);
                if (orders == null)
                {

                    return NotFound();
                }
                _orderServices.delete(orders.id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
