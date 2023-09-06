using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vehicle_task.DTOs
{
    public class GetVehicleMakeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Volkswagen";
        public string Abrv { get; set; }= "VW";
    }
}