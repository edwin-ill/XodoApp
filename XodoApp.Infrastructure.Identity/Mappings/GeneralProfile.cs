using AutoMapper;
using XodoApp.Core.Application.Dtos.UserDtos;
using XodoApp.Infrastructure.Identity.Entities;

namespace XodoApp.Infrastructure.Identity.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
            .ReverseMap()
            .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
            .ForMember(x => x.NormalizedEmail, opt => opt.Ignore())
            .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
            .ForMember(x => x.NormalizedUserName, opt => opt.Ignore())
            .ForMember(x => x.PasswordHash, opt => opt.Ignore())
            .ForMember(x => x.SecurityStamp, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(x => x.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
            .ForMember(x => x.LockoutEnd, opt => opt.Ignore());

            CreateMap<ApplicationUser, UpdateUserDto>()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Role, opt => opt.Ignore())
               .ReverseMap()
                   .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
                   .ForMember(x => x.NormalizedEmail, opt => opt.Ignore())
                   .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
                   .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
                   .ForMember(x => x.NormalizedUserName, opt => opt.Ignore())
                   .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                   .ForMember(x => x.SecurityStamp, opt => opt.Ignore())
                   .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
                   .ForMember(x => x.PhoneNumberConfirmed, opt => opt.Ignore())
                   .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
                   .ForMember(x => x.LockoutEnd, opt => opt.Ignore());


        }
    }
}
