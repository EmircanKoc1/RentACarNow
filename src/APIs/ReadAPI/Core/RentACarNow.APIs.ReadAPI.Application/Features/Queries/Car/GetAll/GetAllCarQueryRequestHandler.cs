﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll
{
    public class GetAllCarQueryRequestHandler : IRequestHandler<GetAllCarQueryRequest, ResponseWrapper<IEnumerable<GetAllCarQueryResponse>>>
    {
        private readonly IMongoCarReadRepository _readRepository;
        private readonly ILogger<GetAllCarQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<IEnumerable<GetAllCarQueryResponse>> _responseBuilder;

        public GetAllCarQueryRequestHandler(
            IMongoCarReadRepository readRepository,
            ILogger<GetAllCarQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<IEnumerable<GetAllCarQueryResponse>> responseBuilder)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public async Task<ResponseWrapper<IEnumerable<GetAllCarQueryResponse>>> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            var paginationParameter = PaginationParameter.CreatePaginationParameter(request.PageNumber, request.PageSize);

            OrderingParameter? orderingParameter = default;

            if (request.SortingField is null)
                orderingParameter = OrderingParameter.CreateOrderingParameter(false, false, string.Empty);
            else
                orderingParameter = OrderingParameter.CreateOrderingParameter(true, request.IsAscending, request.SortingField);


            var cars = await _readRepository.GetAllAsync(
                paginationParameter: paginationParameter,
                filter: c => c.DeletedDate == null,
                orderingParameter: orderingParameter);

            var totalItemCount = await _readRepository.CountAsync();

            var getAllBrandQueryResponses = _mapper.Map<IEnumerable<GetAllCarQueryResponse>>(cars);


            var paginationInfo = new PaginationInfo
            {
                PageItemSize = request.PageSize,
                PageNumber = request.PageNumber,
                TotalItemCount = totalItemCount
            };


            return _responseBuilder
                .SetData(getAllBrandQueryResponses)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .SetPaginationInfo(paginationInfo)
                .Build();
        }


    }
}



