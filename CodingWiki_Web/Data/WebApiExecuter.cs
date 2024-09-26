using Practice5_Model.Models;

namespace Practice5_Web.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {

        private const string apiName = "Practice7Api";
        //private const string authApiName = "AuthorityApi";
        private readonly IHttpClientFactory httpClientFactory;
        //private readonly IConfiguration configuration;
        //private readonly IHttpContextAccessor httpContextAccessor;

        public WebApiExecuter(
            IHttpClientFactory httpClientFactory)
        //IConfiguration configuration,
        //IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            //this.configuration = configuration;
            //this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            
        }
        public async Task InvokeDelete(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.DeleteAsync(relativeUrl);
        }
    }
}
