using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using vehicle_task.DTOs;

namespace vehicle_task
{
    public class AutoMapperProfile : Profile
    {   public AutoMapperProfile()
    {
        CreateMap<VehicleMake,GetVehicleMakeDto>();
        CreateMap<AddVehicleMakeDto, VehicleMake>();

        CreateMap<VehicleModel,GetVehicleModelDto>();
        CreateMap<AddVehicleModelDto, VehicleModel>();
    }
    }
}