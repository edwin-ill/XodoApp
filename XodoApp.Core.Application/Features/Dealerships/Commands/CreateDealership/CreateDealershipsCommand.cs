using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Dealerships.Commands.CreateDealership
{
    public class CreateDealershipsCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El nombre del dealer")]
        public string? Name { get; set; }
        [SwaggerParameter(Description = "La dirección del dealer")]
        public string? Address { get; set; }
        [SwaggerParameter(Description = "La ciudad del dealer")]
        public string? City { get; set; }
        [SwaggerParameter(Description = "El número de telefono del dealer")]
        public string? PhoneNumber { get; set; }
        [SwaggerParameter(Description = "El email del dealer")]
        public string? Email { get; set; }


        public class CreateDealershipsCommandHandler : IRequestHandler<CreateDealershipsCommand, Response<int>>
        {
            private readonly IDealershipRepository _dealershipRepository;
            private readonly IMapper _mapper;

            public CreateDealershipsCommandHandler(IDealershipRepository dealershipRepository, IMapper mapper)
            {
                _dealershipRepository = dealershipRepository;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateDealershipsCommand command, CancellationToken cancellationToken)
            {
                var dealership = _mapper.Map<Dealership>(command);
                await _dealershipRepository.AddAsync(dealership);
                return new Response<int>(dealership.Id);
            }
        }
    }
}
