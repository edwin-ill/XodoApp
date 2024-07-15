using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Dtos.Dealership;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Features.Dealerships.Commands.CreateDealership;
using XodoApp.Core.Application.Features.Dealerships.Commands.UpdateDealership;
using XodoApp.Core.Application.Features.Images.Commands.CreateImage;
using XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle;
using XodoApp.Core.Application.Features.Vehicles.Commands.UpdateVehicle;
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
            #region Vehicle
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Id , opt => opt.Ignore())
                .ReverseMap();
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

            CreateMap<DealershipViewModel, Dealership>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DealershipDto, Dealership>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();
            #endregion
            #region VehicleImage
            CreateMap<SaveVehicleImageViewModel, VehicleImage>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<VehicleImageViewModel, VehicleImage>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<VehicleDto, Vehicle>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();
            #endregion
            #region CQRS  
            #region Vehicle
            CreateMap<CreateVehiclesCommand, Vehicle>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateVehiclesCommand, SaveVehicleViewModel>()
                .ReverseMap();

            CreateMap<VehiclePatchDto, Vehicle>()
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<VehicleImageDto, VehicleImage>()
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ReverseMap();


            CreateMap<UpdateVehicleCommand, Vehicle>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ReverseMap();
            CreateMap<VehicleUpdateResponse, Vehicle>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<VehicleCreateResponse, Vehicle>()
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<VehicleCreateResponse, SaveVehicleViewModel>()
              .ReverseMap();

            #endregion
            #region Dealership
            CreateMap<CreateDealershipsCommand, Dealership>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateDealershipCommand, Dealership>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ReverseMap();
            CreateMap<DealershipUpdateResponse, Dealership>()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ReverseMap();
            #endregion
            #region Image
            CreateMap<CreateImagesCommand, VehicleImage>()
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
