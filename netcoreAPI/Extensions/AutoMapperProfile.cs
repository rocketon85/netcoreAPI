using AutoMapper;
using netcoreAPI.Domains;

namespace netcoreAPI.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateAutoMapperV1();
            CreateAutoMapperV2();          
        }

        public void CreateAutoMapperV1()
        {
            CreateMap<CarDomain, Contracts.Models.Responses.V1.CarViewResponse>()
               .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
               .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
               .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<Contracts.Models.Requests.V1.CarCreateRequest, CarDomain>();
        }

        public void CreateAutoMapperV2()
        {
            CreateMap<CarDomain, Contracts.Models.Responses.V2.CarViewResponse>()
              .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
              .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
              .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name));


            CreateMap<Contracts.Models.Requests.V2.CarCreateRequest, CarDomain>();

        }
    }
}
