using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{

    public class GetByIdBrandQueryRequestHandler : IRequestHandler<GetByIdBrandQueryRequest, ResponseWrapper<GetByIdBrandQueryResponse>>
    {
        private readonly IMongoBrandReadRepository _readRepository;
        private readonly ILogger<GetAllBrandQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetByIdBrandQueryRequestHandler(
            IMongoBrandReadRepository readRepository,
            ILogger<GetAllBrandQueryRequestHandler> logger,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseWrapper<GetByIdBrandQueryResponse>> Handle(GetByIdBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.BrandId);

            if (entity is null)
                return ResponseWrapper<GetByIdBrandQueryResponse>.Success(
                  data: default,
                  statusCode: HttpStatusCode.NotFound,
                  paginationInfo: null);

            var responseData = _mapper.Map<GetByIdBrandQueryResponse>(entity);

            return ResponseWrapper<GetByIdBrandQueryResponse>
                .Success(
                data: responseData,
                statusCode: HttpStatusCode.OK,
                paginationInfo: null);




        }
    }

}
