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
        protected readonly IParkingServices _services;

        public ParkingsController(IParkingServices services)
        {
            //_context = context;
            _services = services;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            var listparking = _services.GetAllParkings();

            ViewData["ParkingIDSortParm"] = sortOrder == "ParkingId" ? "ParkingId_desc" : "ParkingId";
            ViewData["DateTimeInSortParm"] = sortOrder == "DateTimeIn" ? "DateTimeIn_desc" : "DateTimeIn";
            ViewData["DateTimeOutSortParm"] = sortOrder == "DateTimeOut" ? "DateTimeOut_desc" : "DateTimeOut";
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                listparking = listparking.Where(s =>
                                                s.PlateNumber.ToString().ToUpper().Contains(searchString.ToUpper()) ||
                                                s.ParkingSpaceID.ToString().ToUpper().Contains(searchString.ToUpper()) ||
                                                s.EntryPointName.ToString().ToUpper().Contains(searchString.ToUpper()) ||
                                                s.ParkingTypeCode.ToString().ToUpper().Contains(searchString.ToUpper())
                                                ).ToList();
            }

            switch (sortOrder)
            {
                case "ParkingId":
                    listparking = listparking.OrderBy(x => x.ParkingSpaceID).ToList();
                    break;
                case "ParkingId_desc":
                    listparking = listparking.OrderByDescending(x => x.ParkingSpaceID).ToList();
                    break;
                case "DateTimeIn":
                    listparking = listparking.OrderBy(s => s.TimeIn).ToList();
                    break;
                case "DateTimeIn_desc":
                    listparking = listparking.OrderByDescending(s => s.TimeIn).ToList();
                    break;
                case "DateTimeOut":
                    listparking = listparking.OrderBy(s => s.TimeOut).ToList();
                    break;
                case "DateTimeOut_desc":
                    listparking = listparking.OrderByDescending(s => s.TimeOut).ToList();
                    break;
                default:
                    listparking = listparking.OrderByDescending(s => s.ID).ToList();
                    break;
            }

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
