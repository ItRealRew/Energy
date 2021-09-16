using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAPI.Models
{
    public class ViewMeasuring
    {
        public int meterId { get; set; }
        public DateTime value_dt { get; set; }
        public float actR { get; set; }
        public float actO { get; set; }
        public float reactR { get; set; }
        public float reactO { get; set; }
    }
}
