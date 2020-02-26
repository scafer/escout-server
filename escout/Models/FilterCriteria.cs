using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace escout.Models
{
    public class FilterCriteria
    {
        public string fieldName { get; set; }
        public string condition { get; set; }
        public string value { get; set; }
    }
}
