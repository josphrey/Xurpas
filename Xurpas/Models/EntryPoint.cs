using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xurpas.Models
{
    public class EntryPoint
    {
        public int EntryPointID { get; set; }
        [Required]
        [StringLength(250)]
        public string EntryPointName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
