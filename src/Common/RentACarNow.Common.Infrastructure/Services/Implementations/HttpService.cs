using Microsoft.Extensions.Logging;
using RentACarNow.Common.Constants.Http;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace RentACarNow.Common.Infrastructure.Services.Implementations
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _writeHttpClient;
        private readonly HttpClient _readHttpClient;
        private readonly ILogger<HttpService> _logger;
        public HttpService(IHttpClientFactory factory, ILogger<HttpService> logger)
        {
            _writeHttpClient = factory.CreateClient(HttpConstants.WRITE_API_CLIENT_NAME);
            _readHttpClient = factory.CreateClient(HttpConstants.READ_API_CLIENT_NAME);
            _logger = logger;
        }



        public async Task<TResult> DeleteByIdAsync<TResult>(string path, Guid id)
        {
            try
            {
                var result = await _writeHttpClient.DeleteAsync($"{path}/Delete?Id={id}");


            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }

            return default;

        }

        public async Task<TResult> GetAllAsync<TResult>(string path, PaginationParameter paginationParam, OrderingParameter orderingParam)
        {
            var query = $"GetAll?PaginationParameter.PageNumber={paginationParam.PageNumber}&PaginationParameter.Size={paginationParam.Size}&OrderingParameter.Sort={orderingParam.Sort}&OrderingParameter.IsAscending={orderingParam.IsAscending}&OrderingParameter.SortingField={orderingParam.SortingField}";

            TResult result = default;
            try
            {
                var stringResult = await _readHttpClient.GetStringAsync($"{path}/{query}");
                result = stringResult.Deseralize<TResult>();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }

            return result;

        }

        public async Task<TResult?> GetByIdAsync<TResult>(string url, Guid id)
        {

            TResult result = default;
            try
            {

                var repsonseMessage = await _readHttpClient.GetStringAsync($"{url}/GetById?Id={id}");
                result = repsonseMessage.Deseralize<TResult>();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return result;

        }

        public async Task<TResult> PostAsync<TResult, TParam>(string path, TParam param)
            where TParam : class
        {

            try
            {
                var result = await _writeHttpClient.PostAsJsonAsync(path, param);

                if (result.IsSuccessStatusCode)
                    return (await result.Content.ReadAsStringAsync()).Deseralize<TResult>();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }

            return default(TResult);
        }

        public async Task<TResult> PutAsync<TResult, TParam>(string path, TParam param)
            where TParam : class
        {
            try
            {
                var result = await _writeHttpClient.PutAsJsonAsync(path, param);

                if (result.IsSuccessStatusCode)
                    return (await result.Content.ReadAsStringAsync()).Deseralize<TResult>();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }

            return default(TResult);
        }
    }
}
