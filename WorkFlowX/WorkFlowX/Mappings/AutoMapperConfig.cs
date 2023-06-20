using AutoMapper;
using WorkFlowX.Models;
using WorkFlowX.Models.Dtos;

public static class AutoMapperConfig
{
    public static void Initialize()
    {
        Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<License, LicenseDTO>()
            .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.Name));
            cfg.CreateMap<LicenseDTO, License>();

            cfg.CreateMap<Role, RoleDTO>();
            cfg.CreateMap<RoleDTO, Role>();
        });
    }
}
