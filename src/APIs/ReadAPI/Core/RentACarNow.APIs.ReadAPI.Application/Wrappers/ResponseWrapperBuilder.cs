using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Wrappers
{
    public class ResponseBuilder<TData>
    {
        private readonly ResponseWrapper<TData> _responseWrapper;

        //public ResponseBuilder()
        //{
        //    _responseWrapper = new();
        //}

        public ResponseBuilder(ResponseWrapper<TData> responseWrapper)
        {
            _responseWrapper = responseWrapper;
        }

        public ResponseBuilder<TData> SetData(TData data)
        {
            _responseWrapper.Data = data;
            return this;
        }

        public ResponseBuilder<TData> SetHttpStatusCode(HttpStatusCode statusCode)
        {
            _responseWrapper.HttpStatusCode = statusCode;
            return this;
        }

        public ResponseBuilder<TData> SetErrorMessages(IEnumerable<string> errors)
        {
            _responseWrapper.ErrorMessages = errors;
            return this;
        }

        public ResponseBuilder<TData> SetPaginationInfo(PaginationInfo paginationInfo)
        {
            _responseWrapper.PaginationInfo = paginationInfo;
            return this;
        }

        public ResponseWrapper<TData> Build()
        {
            return _responseWrapper;
        }



    }
}
