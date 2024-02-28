using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoDataInfrastructure.Models
{
    public class PlaceCreationRequest : Place
    {
        public Name Name { get; set; }
        public Location Location { get; set; }
    }
}
