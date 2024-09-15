using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Wrappers
{
    public class ResponseWrapper<TData>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public TData? Data { get; set; }
        public PaginationInfo? PaginationInfo { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }



        public static ResponseWrapper<TData> Success(TData data, HttpStatusCode statusCode, PaginationInfo? paginationInfo)
        {
            return new ResponseWrapper<TData>
            {
                Data = data,
                PaginationInfo = paginationInfo,
                HttpStatusCode = statusCode,
                ErrorMessages = default
            };
        }

        public static ResponseWrapper<TData> Fail(HttpStatusCode statusCode, IEnumerable<string> errors)
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
