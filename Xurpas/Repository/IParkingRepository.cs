using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Xurpas.Models;


namespace Xurpas.Repository
{
    public interface IParkingRepository : IGenericRepository<Parking>
    {
        List<Parking> GetAllParking();

        List<ParkingType> GetAllParkingType();
        List<SelectListItem> ListEntryPoint();

        List<SelectListItem> ListVehicleType();

        List<SelectListItem> ListParkingSpace(string entrypoint, string parkingtype);


        ParkingType GetParkingTypeByCode(string code);

        ParkingSpace GetParkingSpaceById(int id);

    }
}
