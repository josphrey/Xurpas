using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using Xurpas.Models.ViewModels;
using Xurpas.Repository;
using Xurpas.Models.Interfaces;
using Xurpas.Data;
using Xurpas.Models;
using Xurpas.Services.Factory;

namespace Xurpas.Services
{
    public class ParkingServices : IParkingServices
    {
        private readonly IParkingRepository _parkingrepository;
        public ParkingServices(IParkingRepository parkingrepository)
        {
            this._parkingrepository = parkingrepository;
        }

        public IQueryable<Parking> GetAll()
        {
            return _parkingrepository.GetAll();
        }

        public List<ParkingViewModel> GetAllParkings()
        {
            List<Parking> lstparkings = _parkingrepository.GetAllParking();
            List<ParkingViewModel> list = new List<ParkingViewModel>();

            if (lstparkings != null)
                foreach (Parking parking in lstparkings)
                {
                    ParkingViewModel viewmodel = new ParkingViewModel();
                    viewmodel.ID = parking.ID;
                    viewmodel.PlateNumber = parking.PlateNumber;
                    viewmodel.EntryPointName = parking.EntryPointName;
                    viewmodel.ParkingTypeCode = parking.ParkingTypeCode;
                    viewmodel.ParkingSpaceID = parking.ParkingSpaceID;
                    viewmodel.TimeIn = parking.TimeIn;
                    viewmodel.TimeOut = parking.TimeOut;
                    viewmodel.IsReturning = parking.IsReturning;
                    viewmodel.NumberOfHours = parking.NumberOfHours;
                    viewmodel.HourlyRate = parking.HourlyRate;
                    viewmodel.TotalParkingFees = parking.TotalParkingFees;

                    DateTime dtTimeOutAddHour = viewmodel.TimeOut.GetValueOrDefault().AddHours(1);
                    DateTime dtcurDatetime = DateTime.Now;

                    int result = DateTime.Compare(dtcurDatetime, dtTimeOutAddHour);

                    if (result <= 0)
                        viewmodel.isDisable = "Yes";
                    else
                        viewmodel.isDisable = "";

                    list.Add(viewmodel);
                }

            return list;
        }

        public ParkingViewModel GetParkingById(int id)
        {
            Parking parking = _parkingrepository.GetbyId(id);
            ParkingViewModel viewmodel = new ParkingViewModel();
            if (parking != null)
            {
                viewmodel.ID = parking.ID;
                viewmodel.PlateNumber = parking.PlateNumber;
                viewmodel.EntryPointName = parking.EntryPointName;
                viewmodel.ParkingTypeCode = parking.ParkingTypeCode;
                viewmodel.ParkingSpaceID = parking.ParkingSpaceID;
                viewmodel.TimeIn = parking.TimeIn;
                viewmodel.TimeOut = parking.TimeOut;
                viewmodel.IsReturning = parking.IsReturning;
                viewmodel.NumberOfHours = parking.NumberOfHours;
                viewmodel.HourlyRate = parking.HourlyRate;
                viewmodel.TotalParkingFees = parking.TotalParkingFees;
            }

            return viewmodel;
        }

        public List<SelectListItem> GetEntryPoint()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _parkingrepository.ListEntryPoint();
            return list;
        }

        public List<SelectListItem> GetParkingByEntryPoint(string entrypoint, string parkingType)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _parkingrepository.ListParkingSpace(entrypoint, parkingType);
            return list;
        }

        public List<SelectListItem> GetVehicleType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _parkingrepository.ListVehicleType();
            return list;
        }

        public void ParkVehicle(ParkingViewModel collection)
        {
            Parking parking = new Parking();
            parking.PlateNumber = collection.PlateNumber;
            parking.EntryPointName = collection.EntryPointName;
            parking.ParkingTypeCode = collection.ParkingTypeCode;
            parking.ParkingSpaceID = collection.ParkingSpaceID;
            parking.TimeIn = collection.TimeIn;

            _parkingrepository.Add(parking);

            UpdateIsActive(collection.ParkingSpaceID, false);
        }

        public void UpdateIsActive(int id, bool isactive)
        {
            _parkingrepository.UpdateParkingAvailability(id, isactive);
        }

        public ParkingViewModel UnParkVehicleDetails(int id)
        {
            IParkingFees parkingfee;
            ParkingViewModel parkingviewmodel = GetParkingById(id);
            ParkingType pt = GetParkingTypeByCode(parkingviewmodel.ParkingTypeCode);

            parkingviewmodel.TimeOut = DateTime.Now;
            switch (parkingviewmodel.ParkingTypeCode)
            {
                case "SP":
                    parkingfee = new ParkingFeesConcreteCreator.SmallVehiclesFactory().ParkingFee();
                    parkingfee.ParkingCode = parkingviewmodel.ParkingTypeCode;
                    parkingfee.TimeIn = parkingviewmodel.TimeIn;
                    parkingfee.TimeOut = parkingviewmodel.TimeOut.GetValueOrDefault();
                    parkingfee.RateFor3Hours = ConstantRates.FlatRate;
                    parkingfee.RateFor24Hours = ConstantRates.Per24hourRate;
                    parkingfee.HourlyRate = pt.HourLyRate;
                    parkingviewmodel.HourlyRate = pt.HourLyRate;
                    parkingviewmodel.NumberOfHours = parkingfee.NumberOfHours();
                    parkingviewmodel.ParkingFeesDetails = parkingfee.SParking();
                    parkingviewmodel.TotalParkingFees = parkingfee.TotalParkingFees();
                    break;
                case "MP":
                    parkingfee = new ParkingFeesConcreteCreator.MediumVehiclesFactory().ParkingFee();
                    parkingfee.ParkingCode = parkingviewmodel.ParkingTypeCode;
                    parkingfee.TimeIn = parkingviewmodel.TimeIn;
                    parkingfee.TimeOut = parkingviewmodel.TimeOut.GetValueOrDefault();
                    parkingfee.RateFor3Hours = ConstantRates.FlatRate;
                    parkingfee.RateFor24Hours = ConstantRates.Per24hourRate;
                    parkingfee.HourlyRate = pt.HourLyRate;
                    parkingviewmodel.HourlyRate = pt.HourLyRate;
                    parkingviewmodel.NumberOfHours = parkingfee.NumberOfHours();
                    parkingviewmodel.ParkingFeesDetails = parkingfee.SParking();
                    parkingviewmodel.TotalParkingFees = parkingfee.TotalParkingFees();

                    break;
                case "LP":
                    parkingfee = new ParkingFeesConcreteCreator.LargeVehiclesFactory().ParkingFee();
                    parkingfee.ParkingCode = parkingviewmodel.ParkingTypeCode;
                    parkingfee.TimeIn = parkingviewmodel.TimeIn;
                    parkingfee.TimeOut = parkingviewmodel.TimeOut.GetValueOrDefault();
                    parkingfee.RateFor3Hours = ConstantRates.FlatRate;
                    parkingfee.RateFor24Hours = ConstantRates.Per24hourRate;
                    parkingfee.HourlyRate = pt.HourLyRate;
                    parkingviewmodel.HourlyRate = pt.HourLyRate;
                    parkingviewmodel.NumberOfHours = parkingfee.NumberOfHours();
                    parkingviewmodel.ParkingFeesDetails = parkingfee.SParking();
                    parkingviewmodel.TotalParkingFees = parkingfee.TotalParkingFees();
                    break;
                default:
                    break;
            }

            return parkingviewmodel;
        }

        public List<ParkingType> GetAllParkingType()
        {
            List<ParkingType> lstParkingTypes = _parkingrepository.GetAllParkingType();
            return lstParkingTypes;
        }

        public ParkingType GetParkingTypeByCode(string code)
        {
            ParkingType pt = new ParkingType();
            pt = _parkingrepository.GetParkingTypeByCode(code);

            return pt;
        }

        public void UnParkVehicle(ParkingViewModel collection)
        {
            Parking parking = new Parking();
            parking.ID = collection.ID;
            parking.PlateNumber = collection.PlateNumber;
            parking.EntryPointName = collection.EntryPointName;
            parking.ParkingTypeCode = collection.ParkingTypeCode;
            parking.ParkingSpaceID = collection.ParkingSpaceID;
            parking.TimeIn = collection.TimeIn;
            parking.TimeOut = collection.TimeOut.GetValueOrDefault();
            parking.HourlyRate = collection.HourlyRate;
            parking.NumberOfHours = collection.NumberOfHours;
            parking.TotalParkingFees = collection.TotalParkingFees;

            _parkingrepository.Update(parking);

            UpdateIsActive(collection.ParkingSpaceID, true);
        }

        public bool isReturningVehicle(ParkingViewModel collection)
        {
            bool isVehicleReturn = collection.IsReturning;

            Parking parking = new Parking();
            parking.ID = collection.ID;
            parking.PlateNumber = collection.PlateNumber;
            parking.EntryPointName = collection.EntryPointName;
            parking.ParkingTypeCode = collection.ParkingTypeCode;
            parking.ParkingSpaceID = collection.ParkingSpaceID;
            parking.IsReturning = collection.IsReturning;
            parking.TimeIn = collection.TimeIn;
            parking.TimeOut = Convert.ToDateTime("0001-01-01 00:00:00.0000000");
            parking.NumberOfHours = 0;
            parking.HourlyRate = 0;
            parking.TotalParkingFees = 0;

            _parkingrepository.Update(parking);

            return isVehicleReturn;
        }

        public ParkingSpace GetParkingSpaceById(int id)
        {
            ParkingSpace ps = new ParkingSpace();
            ps = _parkingrepository.GetParkingSpaceById(id);

            return ps;
        }
    }
}
