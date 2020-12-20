using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xurpas.Models
{
    public class ParkingSpacePerEntryPoint
    {
        public int ID { get; set; }
        [Required]
        public string EntryPointName { get; set; }
        [Required]
        public int ParkingSpaceID { get; set; }
    }
}
