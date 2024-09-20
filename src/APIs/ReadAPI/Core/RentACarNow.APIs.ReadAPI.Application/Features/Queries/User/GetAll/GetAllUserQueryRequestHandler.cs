using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetAll
{
    public class GetAllUserQueryRequestHandler : IRequestHandler<GetAllUserQueryRequest, ResponseWrapper<IEnumerable<GetAllUserQueryResponse>>>
    {
        private readonly IMongoUserReadRepository  _readRepository;
        private readonly ILogger<GetAllUserQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<IEnumerable<GetAllUserQueryResponse>> _responseBuilder;
        private readonly ICustomUserCacheService _cacheService;
        public GetAllUserQueryRequestHandler(
            IMongoUserReadRepository readRepository,
            ILogger<GetAllUserQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<IEnumerable<GetAllUserQueryResponse>> responseBuilder,
            ICustomUserCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<IEnumerable<GetAllUserQueryResponse>>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            OrderingParameter? orderingParameter = default;

            if (request.SortingField is null)
                orderingParameter = OrderingParameter.CreateOrderingParameter(false, false, string.Empty);
            else
                orderingParameter = OrderingParameter.CreateOrderingParameter(true, request.IsAscending, request.SortingField);


            var paginationParameter = PaginationParameter.CreatePaginationParameter(request.PageNumber, request.PageSize);

            var users = await _readRepository.GetAllAsync(
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

            var getAllUserQueryResponses = _mapper.Map<IEnumerable<GetAllUserQueryResponse>>(users);


            return _responseBuilder
                .SetData(getAllUserQueryResponses)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .SetPaginationInfo(paginationInfo)
                .Build();
        }
    }

}
