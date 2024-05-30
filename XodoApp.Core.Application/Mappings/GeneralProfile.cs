using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.ViewModels.Dealerships;
using XodoApp.Core.Application.ViewModels.Users;
using XodoApp.Core.Application.ViewModels.Vehicles;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {

            #region User
            CreateMap<AuthenticationRequest, LoginViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();
            #endregion
            #region CQRS
            #region Vehicle
            CreateMap<SaveVehicleViewModel, Vehicle>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();
            #endregion
            #region Dealership
            CreateMap<SaveDealershipViewModel, Dealership>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();
            #endregion
            #endregion

        }
    }
}
