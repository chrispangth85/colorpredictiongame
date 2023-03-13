using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class DistanceDto
    {
        public string Origin
        {
            get; set;
        }

        public List<string> Destination
        {
            get; set;
        }

        public DistanceDto()
        {
            Destination = new List<string>();
        }
    }
}
