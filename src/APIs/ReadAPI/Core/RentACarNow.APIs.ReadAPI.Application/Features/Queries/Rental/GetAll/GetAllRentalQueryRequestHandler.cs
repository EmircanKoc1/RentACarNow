using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll
{
    public class GetAllRentalQueryRequestHandler : IRequestHandler<GetAllRentalQueryRequest, ResponseWrapper<IEnumerable<GetAllRentalQueryResponse>>>
    {
        private readonly IMongoRentalReadRepository _readRepository;
        private readonly ILogger<GetAllRentalQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<IEnumerable<GetAllRentalQueryResponse>> _responseBuilder;
        private readonly ICustomRentalCacheService _cacheService;
        public GetAllRentalQueryRequestHandler(
            IMongoRentalReadRepository readRepository,
            ILogger<GetAllRentalQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<IEnumerable<GetAllRentalQueryResponse>> responseBuilder,
            ICustomRentalCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<IEnumerable<GetAllRentalQueryResponse>>> Handle(GetAllRentalQueryRequest request, CancellationToken cancellationToken)
        {

            OrderingParameter? orderingParameter = default;

            if (request.SortingField is null)
                orderingParameter = OrderingParameter.CreateOrderingParameter(false, false, string.Empty);
            else
                orderingParameter = OrderingParameter.CreateOrderingParameter(true, request.IsAscending, request.SortingField);


            var paginationParameter = PaginationParameter.CreatePaginationParameter(request.PageNumber, request.PageSize);

            var claims = await _readRepository.GetAllAsync(
                paginationParameter: paginationParameter,
                filter: c => c.DeletedDate == null,
                orderingParameter: orderingParameter);

            long totalItemCount = 0;


            if (_cacheService.GetDbEntityCount() <= 0)
            {
                totalItemCount = await _readRepository.CountAsync();
                _cacheService.SetDbEntityCount(totalItemCount);
            }

            var paginationInfo = new PaginationInfo
            {
                PageItemSize = request.PageSize,
                PageNumber = request.PageNumber,
                TotalItemCount = totalItemCount
            };

            var getAllRentalQueryResponse = _mapper.Map<IEnumerable<GetAllRentalQueryResponse>>(claims);


            return _responseBuilder
                .SetData(getAllRentalQueryResponse)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .SetPaginationInfo(paginationInfo)
                .Build();


        }
    }
}
