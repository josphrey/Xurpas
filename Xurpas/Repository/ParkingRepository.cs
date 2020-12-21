using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Data;
using Xurpas.Models;
using Xurpas.Models.Interfaces;
using Xurpas.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xurpas.Repository
{
    public class ParkingRepository : GenericRepository<Parking>, IParkingRepository
    {
        public ParkingRepository(ParkingContext context) : base(context)
        { }

        public List<Parking> GetAllParking()
        {
            return _context.Parking.ToList();
        }

        public List<SelectListItem> ListEntryPoint()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            _context.EntryPoint.Where(x => x.IsActive == true).ToList().ForEach(x => list.Add(new SelectListItem(x.EntryPointName, x.EntryPointName)));
            return list;
        }

        public List<SelectListItem> ListVehicleType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
             _context.ParkingType.Where(x => x.IsActive == true).ToList().ForEach(x => list.Add(new SelectListItem(x.Description, x.ParkingCode)));
            return list;
        }

        public List<SelectListItem> ListParkingSpace(string entrypoint, string parkingtype)
        {
            ParkingType pt = GetParkingTypeByCode(parkingtype);
            var ptype = pt.Remarks.Split('|');

            List<SelectListItem> list = new List<SelectListItem>();
            if (entrypoint != "" && parkingtype != "")
            {
                var result = (from psep in _context.ParkingSpacePerEntryPoint.Where(x => x.EntryPointName == entrypoint)
                              join ps in _context.ParkingSpace.Where(x => x.IsActive == true && x.IsAvailable == true) on psep.ParkingSpaceID equals ps.ParkingSpaceID
                              where (ptype.Contains(ps.ParkingTypeCode)) //where(ps.ParkingTypeCode == parkingtype)
                              select new ParkingSpace
                              {
                                  ParkingSpaceID = ps.ParkingSpaceID,
                                  ParkingTypeCode = ps.ParkingTypeCode
                              }).ToList();

                result.ForEach(x => list.Add(new SelectListItem(x.ParkingSpaceID.ToString(), x.ParkingSpaceID.ToString())));
            }
            else
            {
                string strmgs = "0";
                list.Add(new SelectListItem(strmgs, strmgs));
            }
            return list;
        }

        public List<ParkingType> GetAllParkingType()
        {
            return _context.ParkingType.ToList();
        }

        public ParkingType GetParkingTypeByCode(string code)
        {
            return _context.ParkingType.Where(x => x.ParkingCode == code).FirstOrDefault();
        }

        public ParkingSpace GetParkingSpaceById(int id)
        {
            return _context.ParkingSpace.Find(id);
        }
    }
}
