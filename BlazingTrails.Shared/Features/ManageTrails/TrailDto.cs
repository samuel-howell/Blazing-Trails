using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Shared.Features.ManageTrails
{
    public class TrailDto // The TrailDto class will be bound to our form to collect values entered by the user
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();

        public class RouteInstruction // is a nested class
        {
            public int Stage { get; set; }
            public string Description { get; set; } = "";
        }
    }
}

    
