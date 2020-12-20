using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xurpas.Models
{
    public class ParkingSpace
    {
        public int ParkingSpaceID { get; set; }
        [Required]
        public string ParkingTypeCode { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public ParkingType ParkingType { get; set; }
    }
}
