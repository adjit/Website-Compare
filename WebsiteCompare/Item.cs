using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCompare
{
    class Item
    {
        public String PartNumber { get; set; }
        public String ManufactuerersNumber { get; set; }
        public String _Origin { get; set; }
        public double MSRP { get; set; }
        public double ResellerCost { get; set; }
    }
}
