using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{
    public class GetByIdRentalQueryRequestHandler : IRequestHandler<GetByIdRentalQueryRequest, ResponseWrapper<GetByIdRentalQueryResponse>>
    {
        private readonly IMongoRentalReadRepository _readRepository;
        private readonly ILogger<GetByIdClaimQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdRentalQueryResponse> _responseBuilder;

        public GetByIdRentalQueryRequestHandler(
            IMongoRentalReadRepository readRepository, 
            ILogger<GetByIdClaimQueryRequestHandler> logger, 
            IMapper mapper, 
            ResponseBuilder<GetByIdRentalQueryResponse> responseBuilder)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public async Task<ResponseWrapper<GetByIdRentalQueryResponse>> Handle(GetByIdRentalQueryRequest request, CancellationToken cancellationToken)
        {
            var responseBuilder = _responseBuilder;

            var entity = await _readRepository.GetByIdAsync(request.RentalId);


            if (entity is null)
            {
                return responseBuilder
                    .SetHttpStatusCode(HttpStatusCode.NotFound)
                    .Build();
            }

            var responseData = _mapper.Map<GetByIdRentalQueryResponse>(entity);


            return responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();

        }
    }

}
