using com.sun.org.apache.xpath.@internal.compiler;
using java.awt;
using Newtonsoft.Json;
using Practice5_Model.Models;
using System.Net.Http.Headers;

namespace Practice5_Web.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {

        private const string apiName = "Practice7Api";
        private const string authApiName = "AuthorityApi";
        //private const string authApiName = "AuthorityApi";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        //private readonly IHttpContextAccessor httpContextAccessor;

        public WebApiExecuter(
            IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
        //IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            //this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            
        }
        public async Task InvokeDelete(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.DeleteAsync(relativeUrl);
        }

        public async Task AddJwtToHeader(HttpClient httpClient)
        {
            var clientId = configuration.GetValue <string>("ClientId");
            var secret = configuration.GetValue <string>("Secret");
            
            //Authenticate
            var authoClient = httpClientFactory.CreateClient(authApiName);
            var response = await authoClient.PostAsJsonAsync("auth", new WebAppCredential
            {
                ClientId = clientId,
                Secret = secret
            });
            response.EnsureSuccessStatusCode();

            //Get JWT
            string strToken = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strToken);

            //Pass JWT to Endpoints
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}
