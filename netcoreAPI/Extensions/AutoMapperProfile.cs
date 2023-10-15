using AutoMapper;
using netcoreAPI.Domain;
using netcoreAPI.Models;

namespace netcoreAPI.Extensions
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {
            CreateMap<Car, CarViewModel>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<CarCreate, Car>();
                
        }
    }
}
