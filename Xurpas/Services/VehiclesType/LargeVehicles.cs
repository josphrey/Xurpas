using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xurpas.Models.Interfaces;

namespace Xurpas.Services.Factory
{
    public class LargeVehicles : IParkingFees
    {
        public string ParkingCode { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public decimal RateFor3Hours { get; set; }
        public decimal RateFor24Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public int NumberOfHours()
        {
            TimeSpan ts = (TimeOut - TimeIn);
            int hr = 0;
            hr = (int)Math.Round(ts.TotalHours);

            return hr;
        }

        public string SParking()
        {
            string strParking = string.Empty;

            int num = NumberOfHours();
            if (num < 24)
            {
                if (num <= 3)
                {
                    strParking = string.Format(@"{0}", RateFor3Hours);
                }
                else
                {
                    int hr = (num - 3);

                    strParking = string.Format(@"{0} + ({1} x {2})", RateFor3Hours, hr, HourlyRate);
                }
            }
            else if (num >= 24)
            {
                if (num == 24)
                {
                    strParking = string.Format(@"{0}", RateFor24Hours);
                }
                else
                {
                    int iTotalDays = num / 24;
                    int iRemainingHours = num % 24;
                    strParking = string.Format(@"({0} x {1} day/s) + ({2} x {3} hour/s)", RateFor24Hours, iTotalDays, HourlyRate, iRemainingHours);
                }
            }

            return strParking;
        }

        public decimal TotalParkingFees()
        {
            decimal dTotalParkingFees = 0;
            decimal dTotalAdditionalHourRate = 0;
            decimal dTotalDaysRate = 0;
            int num = NumberOfHours();

            if (num < 24)
            {
                if (num <= 3)
                {
                    dTotalParkingFees = RateFor3Hours;
                }
                else
                {
                    int hr = (num - 3);
                    for (int i = 0; i < hr; i++)
                    {
                        dTotalAdditionalHourRate += HourlyRate;
                    }
                    dTotalParkingFees = (dTotalAdditionalHourRate + RateFor3Hours);
                }
            }
            else if (num >= 24)
            {
                if (num == 24)
                {
                    dTotalParkingFees = RateFor24Hours;
                }
                else
                {
                    int iTotalDays = num / 24;
                    int iRemainingHours = num % 24;

                    for (int i = 0; i < iTotalDays; i++)
                    {
                        dTotalDaysRate += RateFor24Hours;
                    }

                    for (int i = 0; i < iRemainingHours; i++)
                    {
                        dTotalAdditionalHourRate += HourlyRate;
                    }
                    dTotalParkingFees = (dTotalDaysRate + dTotalAdditionalHourRate);
                }
            }

            return Decimal.Round(dTotalParkingFees, 2);
        }
    }
}
