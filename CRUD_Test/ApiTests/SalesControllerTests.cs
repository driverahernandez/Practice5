using Microsoft.AspNetCore.Mvc;
using Moq;
using Practice5_DataAccess.Data.RepositoryFactory;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice5_WebAPI.Controllers;
using sun.security.x509;
using System.Net.Http.Json;
using System.Net.Http;
using Newtonsoft.Json;
using Practice5_Web.Data;
using System.Net.Http.Headers;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace Practice5.Test.ApiTests
{
    public class SalesControllerTests : IDisposable
    {
        public IRepositorySales _salesRepository;
        public SaleController _saleController;

        public int invalidId = 100;
        public int validId = 2;
        public DateOnly originalDate = new DateOnly(2024, 8, 30);
        DateOnly newDate = new DateOnly(2024, 9, 30);

        public string token; 

        // tests to be performed in order to
        // first be authenticated,
        // then create a new item, which will be edited and finally deleted
        // in the end the original list of items wont be affected. 
        [SetUp]
        public void Setup()
        {

        }

        // test for AUTHENTICATION
        [Test]
        [Order(1)]
        public void GetToken()
        {
            var credential = new
            {
                ClientId = "MVCWebAppPractice7",
                Secret = "123ABC456"
            };

            var jsonCredential = JsonConvert.SerializeObject(credential);
            var data = new StringContent(jsonCredential, Encoding.UTF8, "application/json");

            var url = "https://localhost:7117/auth";
            using var client = new HttpClient();

            var response = client.PostAsync(url, data).Result;

            string strAuthResponse = response.Content.ReadAsStringAsync().Result;
            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(strAuthResponse);
            token = authResponse.AccessToken;
            
        }


        // tests for GET action
        [Test]
        [Order(2)]
        public void GetSales_ReturnsStatusCode200()
        {
            var url = "https://localhost:7117/api/Sale";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync(url).Result;
            Assert.True(response.IsSuccessStatusCode);
        }

        [Test]
        [Order(3)]
        public void GetSales_ReturnsListOfSales()
        {
            var url = "https://localhost:7117/api/Sale";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync(url).Result;
            var content = response.Content.ReadFromJsonAsync<List<Sale>>().Result;
            Assert.NotNull(content);
            Assert.That(content, Has.Exactly(10).Items);
        }

        [Test]
        [Order(3)]
        public void GetSales_Given_ValidId_ReturnsSingleSale()
        {
            var url = $"https://localhost:7117/api/Sale/{validId}";
            
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = client.GetAsync(url).Result;
            var content = response.Content.ReadFromJsonAsync<Sale>().Result;
            Assert.NotNull(content);
            Assert.That(content.SaleId, Is.EqualTo(validId));

        }

        // test for POST action
        [Test]
        [Order(4)]
        public void CreateSale_ReturnsStatusCode200()
        {
            var url = $"https://localhost:7117/api/Sale";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                // include authorization in header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // include sale data in body
                var sale1 = new Sale { ProductId = 1, SaleDate = originalDate, Total = 11.1 };
                var jsonSale = JsonConvert.SerializeObject(sale1);
                var data = new StringContent(jsonSale, Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, data).Result;


                var getAllSales = client.GetAsync(url).Result;
                var sales = getAllSales.Content.ReadFromJsonAsync<List<Sale>>().Result;
                var lastSale = sales[sales.Count()-1];
                var lastSaleId = lastSale.SaleId;

                Assert.That(lastSaleId, Is.GreaterThan(10));
                
                validId = lastSaleId;
            }
        }


        // tests for PUT action
        [Test]
        [Order(5)]
        public void UpdateSale_Given_ValidId_ReturnsStatusCode200()
        {
            var url = $"https://localhost:7117/api/Sale/{validId}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                // include authorization in header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // include update data in body
                var sale1 = new Sale { ProductId = 1, SaleDate = newDate, Total = 11.1 };
                var jsonSale = JsonConvert.SerializeObject(sale1);
                var data = new StringContent(jsonSale, Encoding.UTF8, "application/json");
                var response = client.PutAsync(url, data).Result;

                Assert.True(response.IsSuccessStatusCode);
            }
        }

        [Test]
        [Order(6)]
        public void UpdateSale_Given_ValidId_ChangesData()
        {
            var url = $"https://localhost:7117/api/Sale/{validId}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            //test changes made in previous method.
            var response = client.GetAsync(url).Result;
            var updatedSale = response.Content.ReadFromJsonAsync<Sale>().Result;
            Assert.That(updatedSale.SaleDate, Is.EqualTo(newDate));
        }

        // test for DELETE action
        [Test]
        [Order(7)]
        public void DeleteSale_Given_ValidId_DeletesSale()
        {
            var url = $"https://localhost:7117/api/Sale/{validId}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = client.DeleteAsync(url).Result;
            Assert.True(response.IsSuccessStatusCode);
        }
        public void Dispose()
        {
            //
        }
    }
}
