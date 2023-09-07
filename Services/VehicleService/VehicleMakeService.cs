using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vehicle_task.Data;
using vehicle_task.DTOs;

namespace vehicle_task.Services.VehicleService
{
    public class VehicleMakeService : IVehicleMakeService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public VehicleMakeService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetVehicleMakeDto>>> AddVehicleMake(AddVehicleMakeDto newVehicleMake)
        {
            var serviceResponse = new ServiceResponse<List<GetVehicleMakeDto>>();
            try
            {
                var vehicleMake = _mapper.Map<VehicleMake>(newVehicleMake);
                _context.VehicleMakes.Add(vehicleMake);
                vehicleMake = await _context.VehicleMakes.FirstOrDefaultAsync(c => c.Id == vehicleMake.Id);
                serviceResponse.Data = new List<GetVehicleMakeDto> { _mapper.Map<GetVehicleMakeDto>(vehicleMake) };
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to add vehicle make: " + ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVehicleMakeDto>>> DeleteVehicleMake(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetVehicleMakeDto>>();
            try
            {

                var vehicleMake = await _context.VehicleMakes.FirstOrDefaultAsync(c => c.Id == id);
                if (vehicleMake is null)
                    throw new Exception($"Vehicle make with Id '{id}' not found.");

                _context.VehicleMakes.Remove(vehicleMake);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.VehicleMakes
            .Select(c => _mapper.Map<GetVehicleMakeDto>(c))
            .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVehicleMakeDto>>> GetAllVehicleMakes(string searchTerm= null , string sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var serviceResponse = new ServiceResponse<List<GetVehicleMakeDto>>();
            try
            {
                var query = _context.VehicleMakes.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    query = query.Where(vm => vm.Name.ToLower().Contains(searchTerm) || vm.Abrv.ToLower().Contains(searchTerm));
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.ToLower())
                    {
                        case "name":
                            query = ascending ? query.OrderBy(vm => vm.Name) : query.OrderByDescending(vm => vm.Name);
                            break;
                        case "id":
                            query = ascending ? query.OrderBy(vm => vm.Id) : query.OrderByDescending(vm => vm.Id);
                            break;
                        case "abrv":
                            query = ascending ? query.OrderBy(vm => vm.Abrv) : query.OrderByDescending(vm => vm.Abrv);
                            break;
                        default:
                            break;
                    }
                }
                //straničenje
                int skip = (pageNumber - 1) * pageSize;
                int take = pageSize;
                query = query.Skip(skip).Take(take);
                var dbCharacters = await query.ToListAsync();
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetVehicleMakeDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to retrieve vehicle makes: " + ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<GetVehicleMakeDto>> GetVehicleMakeById(int id)
        {
            var serviceResponse = new ServiceResponse<GetVehicleMakeDto>();
            var dbCharacter = await _context.VehicleMakes.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetVehicleMakeDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetVehicleMakeDto>> UpdateVehicleMake(UpdateVehicleMakeDto updatedVehicleMake)
        {
            var serviceResponse = new ServiceResponse<GetVehicleMakeDto>();

            try
            {
                var vehicleMake = await _context.VehicleMakes.FirstOrDefaultAsync(c => c.Id == updatedVehicleMake.Id);

                if (vehicleMake is null)
                    throw new Exception($"Vehicle make with Id '{updatedVehicleMake.Id}' not found.");
                vehicleMake.Name = updatedVehicleMake.Name;
                vehicleMake.Abrv = updatedVehicleMake.Abrv;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetVehicleMakeDto>(vehicleMake);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }
    }
}