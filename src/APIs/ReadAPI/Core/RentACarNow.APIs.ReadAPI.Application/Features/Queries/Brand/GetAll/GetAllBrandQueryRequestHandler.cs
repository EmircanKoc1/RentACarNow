using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;
using System.Net;
using ME = RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll
{
    public class GetAllBrandQueryRequestHandler : IRequestHandler<GetAllBrandQueryRequest, ResponseWrapper<IEnumerable<GetAllBrandQueryResponse>>>
    {
        private readonly IMongoBrandReadRepository _readRepository;
        private readonly ILogger<GetAllBrandQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomBrandCacheService _cacheService;
        public GetAllBrandQueryRequestHandler(
            IMongoBrandReadRepository readRepository,
            ILogger<GetAllBrandQueryRequestHandler> logger,
            IMapper mapper,
            ICustomBrandCacheService brandCacheService,
            ICustomBrandCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<IEnumerable<GetAllBrandQueryResponse>>> Handle(GetAllBrandQueryRequest request, CancellationToken cancellationToken)
        {

            var paginationParameter = PaginationParameter.CreatePaginationParameter(request.PageNumber, request.PageSize);

            OrderingParameter? orderingParameter = default;

            IEnumerable<ME.Brand?> entities = default;


            if (request.SortingField is null)
            {
                orderingParameter = OrderingParameter.CreateOrderingParameter(false, false, string.Empty);
            }
            else
                orderingParameter = OrderingParameter.CreateOrderingParameter(true, request.IsAscending, request.SortingField);


            entities = await _readRepository.GetAllAsync(
                paginationParameter: paginationParameter,
                filter: b => b.DeletedDate == null,
                orderingParameter: orderingParameter);

            long totalItemCount = 0;

            if (_cacheService.GetDbEntityCount() <= 0)
            {
                totalItemCount = await _readRepository.CountAsync();
                _cacheService.SetDbEntityCount(totalItemCount);
            }

            var getAllBrandQueryResponses = _mapper.Map<IEnumerable<GetAllBrandQueryResponse>>(entities);


            var paginationInfo = new PaginationInfo
            {
                PageItemSize = request.PageSize,
                PageNumber = request.PageNumber,
                TotalItemCount = totalItemCount
            };


            return ResponseWrapper<IEnumerable<GetAllBrandQueryResponse>>
                .Success(getAllBrandQueryResponses, HttpStatusCode.OK, paginationInfo);
        }
    }
}
