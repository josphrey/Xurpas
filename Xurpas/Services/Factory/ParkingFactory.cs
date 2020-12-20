using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Models;
using Xurpas.Models.Interfaces;
namespace Xurpas.Services.Factory
{
    public abstract class ParkingFactory
    {
        protected abstract IParkingFees ParkingFees();

        public IParkingFees ParkingFee()
        {
            return this.ParkingFees();
        }
    }
}
