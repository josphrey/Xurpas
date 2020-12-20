using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xurpas.Models.Interfaces
{
    public interface IParkingFees
    {
        string ParkingCode { get; set; }
        DateTime TimeIn { get; set; }
        DateTime TimeOut { get; set; }
        decimal RateFor3Hours { get; set; }
        decimal RateFor24Hours { get; set; }
        decimal HourlyRate { get; set; }
        int NumberOfHours();
        string SParking();
        decimal TotalParkingFees();
    }
}
