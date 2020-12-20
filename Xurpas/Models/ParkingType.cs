using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xurpas.Models
{
    public class ParkingType
    {
        public int ParkingTypeID { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ParkingCode { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal HourLyRate { get; set; }
        public bool IsActive { get; set; }
    }
}
