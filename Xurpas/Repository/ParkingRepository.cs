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
            List<Parking> lst = _context.Parking.ToList();
            return lst;
        }

        public List<SelectListItem> ListEntryPoint()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<EntryPoint> ep = _context.EntryPoint.Where(x => x.IsActive == true).ToList();
            ep.ForEach(x => list.Add(new SelectListItem(x.EntryPointName, x.EntryPointName)));
            return list;
        }

        public List<SelectListItem> ListVehicleType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<ParkingType> pt = _context.ParkingType.Where(x => x.IsActive == true).ToList();
            pt.ForEach(x => list.Add(new SelectListItem(x.Description, x.ParkingCode)));
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
                              where (ptype.Contains(ps.ParkingTypeCode))
                              select new ParkingSpace
                              {
                                  ParkingSpaceID = ps.ParkingSpaceID,
                                  ParkingTypeCode = ps.ParkingTypeCode
                              }).ToList();
                //where(ps.ParkingTypeCode == parkingtype)
                result.ForEach(x => list.Add(new SelectListItem(x.ParkingSpaceID.ToString(), x.ParkingSpaceID.ToString())));
            }
            else
            {
                string strmgs = "0";
                list.Add(new SelectListItem(strmgs, strmgs));
            }
            return list;
        }

        public void UpdateParkingAvailability(int parkingSpaceId, bool isActive)
        {
            ParkingSpace ps = _context.ParkingSpace.Find(parkingSpaceId);
            ps.IsAvailable = isActive;
            _context.ParkingSpace.Update(ps);
            _context.SaveChanges();
        }

        public List<ParkingType> GetAllParkingType()
        {
            List<ParkingType> lst = _context.ParkingType.ToList();
            return lst;
        }

        public ParkingType GetParkingTypeByCode(string code)
        {
            ParkingType pt = new ParkingType();
            pt = _context.ParkingType.Where(x => x.ParkingCode == code).FirstOrDefault();
            return pt;
        }

        public ParkingSpace GetParkingSpaceById(int id)
        {
            ParkingSpace ps = new ParkingSpace();
            ps = _context.ParkingSpace.Find(id);
            return ps;
        }
    }
}
