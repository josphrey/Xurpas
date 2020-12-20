using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Models;
using Xurpas.Models.Interfaces;

namespace Xurpas.Services.Factory
{
    public class ParkingFeesConcreteCreator
    {
        public class SmallVehiclesFactory : ParkingFactory
        {
            protected override IParkingFees ParkingFees()
            {
                IParkingFees parkingFees = new SmallVehicles();
                return parkingFees;
            }
        }

        public class MediumVehiclesFactory : ParkingFactory
        {
            protected override IParkingFees ParkingFees()
            {
                IParkingFees parkingFees = new MediumVehicles();
                return parkingFees;
            }
        }

        public class LargeVehiclesFactory : ParkingFactory
        {
            protected override IParkingFees ParkingFees()
            {
                IParkingFees parkingFees = new LargeVehicles();
                return parkingFees;
            }
        }
    }
}
