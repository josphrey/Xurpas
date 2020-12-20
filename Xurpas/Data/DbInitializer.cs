using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Models;


namespace Xurpas.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ParkingContext context)
        {
            context.Database.EnsureCreated();

            if (context.ParkingType.Any())
            {
                return;
            }

            var parkingtypes = new ParkingType[]
            {
                new ParkingType{ParkingCode="SP",Description="Small Vehicles", Remarks="SP|MP|LP", HourLyRate = 20, IsActive = true},
                new ParkingType{ParkingCode="MP",Description="Medium Vehicles", Remarks="MP|LP", HourLyRate = 60, IsActive = true},
                new ParkingType{ParkingCode="LP",Description="Large Vehicles", Remarks="LP", HourLyRate = 100, IsActive = true},
                new ParkingType{ParkingCode="XP",Description="Extra Large Vehicles", Remarks="XP", HourLyRate = 200, IsActive = false}
            };
            foreach (ParkingType pt in parkingtypes)
            {
                context.ParkingType.Add(pt);
            }
            context.SaveChanges();

            var entrypoints = new EntryPoint[]
            {
                new EntryPoint{EntryPointName = "Main", Description = "Main Entry Point", IsActive = true },
                new EntryPoint{EntryPointName = "Back", Description = "Back Entry Point", IsActive = true },
                new EntryPoint{EntryPointName = "Right", Description = "Right Entry Point", IsActive = true }
            };
            foreach (EntryPoint ep in entrypoints)
            {
                context.EntryPoint.Add(ep);
            }
            context.SaveChanges();

            var parkingspace = new ParkingSpace[]
            {
                new ParkingSpace{ParkingTypeCode = "SP", IsAvailable = false, IsActive = true }, //1
                new ParkingSpace{ParkingTypeCode = "SP", IsAvailable = false, IsActive = true }, //2
                new ParkingSpace{ParkingTypeCode = "MP", IsAvailable = true, IsActive = true }, //3
                new ParkingSpace{ParkingTypeCode = "MP", IsAvailable = true, IsActive = true }, //4
                new ParkingSpace{ParkingTypeCode = "LP", IsAvailable = true, IsActive = true }, //5

                new ParkingSpace{ParkingTypeCode = "MP", IsAvailable = true, IsActive = true }, //6
                new ParkingSpace{ParkingTypeCode = "SP", IsAvailable = true, IsActive = true }, //7
                new ParkingSpace{ParkingTypeCode = "SP", IsAvailable = true, IsActive = true }, //8
                new ParkingSpace{ParkingTypeCode = "SP", IsAvailable = true, IsActive = true }, //9 
                new ParkingSpace{ParkingTypeCode = "SP", IsAvailable = true, IsActive = true }, //10

                new ParkingSpace{ParkingTypeCode = "MP", IsAvailable = true, IsActive = true }, //11
                new ParkingSpace{ParkingTypeCode = "LP", IsAvailable = true, IsActive = true }, //12
                new ParkingSpace{ParkingTypeCode = "LP", IsAvailable = true, IsActive = true }, //13
                new ParkingSpace{ParkingTypeCode = "LP", IsAvailable = true, IsActive = true }, //14
                new ParkingSpace{ParkingTypeCode = "MP", IsAvailable = true, IsActive = true }  //15
            };
            foreach (ParkingSpace ps in parkingspace.OrderBy(x => x.IsAvailable))
            {
                context.ParkingSpace.Add(ps);
            }
            context.SaveChanges();

            var parkingspaceperentrypoint = new ParkingSpacePerEntryPoint[]
            {
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 1},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 2},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 3},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 4},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 5},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 6},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 7},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 8},
                new ParkingSpacePerEntryPoint{EntryPointName = "Main", ParkingSpaceID = 9},

                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 4},
                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 5},
                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 8},
                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 9},
                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 10},
                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 14},
                new ParkingSpacePerEntryPoint{EntryPointName = "Right", ParkingSpaceID = 15},

                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 7},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 8},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 9},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 11},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 12},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 13},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 14},
                new ParkingSpacePerEntryPoint{EntryPointName = "Back", ParkingSpaceID = 15},
            };
            foreach (ParkingSpacePerEntryPoint psep in parkingspaceperentrypoint.OrderBy(x => x.ParkingSpaceID).ThenBy(x => x.EntryPointName))
            {
                context.ParkingSpacePerEntryPoint.Add(psep);
            }
            context.SaveChanges();

            var parking = new Parking[]
            {
                new Parking{ ParkingSpaceID = 1, ParkingTypeCode = "SP",  EntryPointName = "Main", TimeIn = DateTime.Now.AddDays(-1), PlateNumber = "XYZ1234"},
                new Parking{ ParkingSpaceID = 2, ParkingTypeCode = "SP",  EntryPointName = "Main", TimeIn = DateTime.Now.AddHours(-3), PlateNumber = "ZXY4321"},
            };
            foreach (Parking p in parking)
            {
                context.Parking.Add(p);
            }
            context.SaveChanges();
        }
    }
}
