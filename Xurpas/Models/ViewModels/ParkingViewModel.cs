using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xurpas.Models.ViewModels
{
    public class ParkingViewModel
    {
        public ParkingViewModel()
        {
            ID = 0;
            PlateNumber = "";
            EntryPointName = "";
            ParkingTypeCode = "";
            ParkingSpaceID = 0;
            TimeIn = DateTime.Now;
            TimeOut = Convert.ToDateTime("01/01/0001 12:00:00 am");
            IsReturning = false;
            NumberOfHours = 0;
            HourlyRate = 0;
            ParkingFeesDetails = "";
            TotalParkingFees = 0;
        }

        public int ID { get; set; }
        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public string EntryPointName { get; set; }
        [Required]
        public string ParkingTypeCode { get; set; }
        [Required]
        public int ParkingSpaceID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> TimeOut { get; set; }
        public string isDisable { get; set; }
        public bool IsReturning { get; set; }
        public int NumberOfHours { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal HourlyRate { get; set; }
        public string ParkingFeesDetails { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal TotalParkingFees { get; set; }
    }
}
