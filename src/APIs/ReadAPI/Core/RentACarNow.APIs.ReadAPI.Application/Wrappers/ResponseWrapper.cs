using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Wrappers
{
    public class ResponseWrapper<TData>
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
        public TData? Data { get; set; } = default;
        public PaginationInfo? PaginationInfo { get; set; } = default;
        public IEnumerable<string>? ErrorMessages { get; set; } = default;



        //public static ResponseBuilder<TData> Builder() => new ResponseBuilder<TData>();


        public static ResponseWrapper<TData> Success(
            TData? data, 
            HttpStatusCode statusCode, 
            PaginationInfo? paginationInfo)
        {
            return new ResponseWrapper<TData>
            {
                Data = data,
                PaginationInfo = paginationInfo,
                HttpStatusCode = statusCode,
                ErrorMessages = default
            };
        }

        public static ResponseWrapper<TData> Fail(
            HttpStatusCode statusCode, 
            IEnumerable<string> errors)
        {
            return new ResponseWrapper<TData>
            {
                Data = default,
                PaginationInfo = default,
                ErrorMessages = errors,
                HttpStatusCode = statusCode,
            };
        }



    }




}
