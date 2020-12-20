using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Models;
using Xurpas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xurpas.Services
{
    public interface IParkingServices
    {
        IQueryable<Parking> GetAll();

        List<ParkingViewModel> GetAllParkings();

        List<ParkingType> GetAllParkingType();

        ParkingViewModel GetParkingById(int id);

        List<SelectListItem> GetEntryPoint();

        List<SelectListItem> GetVehicleType();

        List<SelectListItem> GetParkingByEntryPoint(string entrypoint, string parkingtype);

        void UpdateIsActive(int id, bool isactive);

        void ParkVehicle(ParkingViewModel collection);

        void UnParkVehicle(ParkingViewModel collection);

        bool isReturningVehicle(ParkingViewModel collection);

        ParkingViewModel UnParkVehicleDetails(int id);

        ParkingType GetParkingTypeByCode(string code);

        ParkingSpace GetParkingSpaceById(int id);
    }
}
