using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;
using System.Text;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{

    public class GetByIdBrandQueryRequestHandler : IRequestHandler<GetByIdBrandQueryRequest, ResponseWrapper<GetByIdBrandQueryResponse>>
    {
        private readonly IMongoBrandReadRepository _readRepository;
        private readonly ILogger<GetAllBrandQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdBrandQueryResponse> _responseBuilder;
        public GetByIdBrandQueryRequestHandler(
            IMongoBrandReadRepository readRepository,
            ILogger<GetAllBrandQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdBrandQueryResponse> responseBuilder)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public async Task<ResponseWrapper<GetByIdBrandQueryResponse>> Handle(GetByIdBrandQueryRequest request, CancellationToken cancellationToken)
        {

            var entity = await _readRepository.GetByIdAsync(request.BrandId);

            //var responseBuilder = ResponseWrapper<GetByIdBrandQueryResponse>
            //    .Builder();

            var responseBuilder = _responseBuilder;

            if (entity is null)
            {
                return responseBuilder
                    .SetHttpStatusCode(HttpStatusCode.NotFound)
                    .Build();
            }

            var responseData = _mapper.Map<GetByIdBrandQueryResponse>(entity);

            
            return responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();


        }
    }

}
