using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Models.Interfaces;

namespace Xurpas.Services.Factory
{
    public class ExtraLargeVehicles : IParkingFees
    {
        public string ParkingCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime TimeIn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime TimeOut { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal HourlyRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal RateFor3Hours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal RateFor24Hours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int NumberOfHours()
        {
            throw new NotImplementedException();
        }

        public string SParking()
        {
            throw new NotImplementedException();
        }

        public decimal TotalParkingFees()
        {
            throw new NotImplementedException();
        }
    }
}
