using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xurpas.Models
{
    public class Parking
    {
        public int ID { get; set; }
        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public int ParkingSpaceID { get; set; }
        [Required]
        public string ParkingTypeCode { get; set; }
        [Required]
        public string EntryPointName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeOut { get; set; }
        public bool IsReturning { get; set; }
        public int NumberOfHours { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal HourlyRate { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal TotalParkingFees { get; set; }
    }
}
