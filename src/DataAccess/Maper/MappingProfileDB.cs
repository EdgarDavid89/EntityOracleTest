using AutoMapper;
using DataAccess.Models;
using DataAccess.DTO;

namespace DataAccess.Maper
{
    public class MappingProfileDB : Profile
    {
        public MappingProfileDB()
        {
            CreateMap<Worker, WorkerDto>().ReverseMap();
            CreateMap<Parking, ParkingDto>().ReverseMap();
            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}