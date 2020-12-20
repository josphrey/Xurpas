using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xurpas.Data;
using Xurpas.Models.ViewModels;
using Xurpas.Services;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Xurpas.Controllers
{
    public class ParkingsController : Controller
    {
        //private readonly ParkingContext _context; ParkingContext context, 
        private readonly IParkingServices _services;

        public ParkingsController(IParkingServices services)
        {
            //_context = context;
            _services = services;
        }

        public IActionResult Index()
        {
            var listparking = _services.GetAllParkings();
            return View(listparking);
        }

        public IActionResult Park()
        {
            ParkingViewModel parkingviewmodel = new ParkingViewModel();
            parkingviewmodel.TimeIn = DateTime.Now;
            PopulateListEntryPoint();
            PopulateVehicleType();

            return View(parkingviewmodel);
        }

        [HttpPost]
        public IActionResult Park(ParkingViewModel collection)
        {
            if (ModelState.IsValid)
                _services.ParkVehicle(collection);
            else
                return RedirectToAction("Park");

            return RedirectToAction("Index");
        }

        public IActionResult UnPark(int? id)
        {
            ParkingViewModel parkingviewmodel = new ParkingViewModel();
            if (id != null)
            {
                parkingviewmodel = _services.UnParkVehicleDetails(id.GetValueOrDefault());
            }

            return View(parkingviewmodel);
        }

        [HttpPost]
        public IActionResult UnPark(ParkingViewModel collection)
        {
            if (ModelState.IsValid)
                _services.UnParkVehicle(collection);
            else
                return RedirectToAction("Index");

            return RedirectToAction("Index");
        }

        public IActionResult Returning(int? id)
        {
            ParkingViewModel parkingviewmodel = new ParkingViewModel();
            parkingviewmodel = _services.GetParkingById(id.GetValueOrDefault());

            return View(parkingviewmodel);
        }

        [HttpPost]
        public IActionResult Returning(ParkingViewModel collection)
        {
            if (ModelState.IsValid)
            {
                Xurpas.Models.ParkingSpace ps = _services.GetParkingSpaceById(collection.ParkingSpaceID);
                if (ps.IsAvailable == true)
                {
                    _services.isReturningVehicle(collection);
                }
                else
                {
                    ModelState.AddModelError("Returning", "Parking is not available");
                    TempData["FormData"] = collection;
                    return RedirectToAction("Returning", new { id = collection.ID });
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            ParkingViewModel parkingviewmodel = new ParkingViewModel();
            parkingviewmodel = _services.GetParkingById(id);

            return View(parkingviewmodel);
        }

        #region Dropdown List   
        public JsonResult GetlistParking(string entrypoint, string parkingtype)
        {
            List<SelectListItem> list = _services.GetParkingByEntryPoint(entrypoint, parkingtype);
            return Json(list);
        }

        private void PopulateListEntryPoint()
        {
            List<SelectListItem> list = _services.GetEntryPoint();
            ViewBag.ParkingEntryPoint = list;
        }

        private void PopulateVehicleType()
        {
            List<SelectListItem> list = _services.GetVehicleType();
            ViewBag.ParkingVehicleType = list;
        }
        #endregion
    }
}
