using AutoMapper;
using netcoreAPI.Domains;

namespace netcoreAPI.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CarDomain, Models.V1.CarViewModel>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<Models.V1.CarCreateModel, CarDomain>();

            CreateMap<CarDomain, Models.V2.CarViewModel>()
               .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
               .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
               .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<Models.V2.CarCreateModel, CarDomain>();
        }
    }
}
