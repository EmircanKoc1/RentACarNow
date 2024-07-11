using Microsoft.Extensions.Logging;
using RentACarNow.Common.Constants.Http;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
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

        public async Task<TResult> DeleteAsync<TResult, TParam>(string path, TParam param)
            where TParam : class
        {


            await _writeHttpClient.DeleteAsync($"{path}/{param}");
            throw new Exception();

        }

        public Task<TResult> GetAsync<TResult, TParam>(string path, TParam param)
            where TParam : class
        {
            throw new Exception();

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
