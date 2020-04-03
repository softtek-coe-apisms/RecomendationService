using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecommendationService.Models
{
    public class ProductCatalogServiceAPI
    {
        private HttpClient products;
        private Task<HttpResponseMessage> responseMessage;
        private string productsBaseUrl;
        private const string PRODUCTS_PAGE = "ProductCatalogService?pageNumber=";
        private const string PRODUCTS_NAME = "ProductCatalogService?name=";
        private const string PRODUCTS_PRODUCT_ID = "ProductCatalogService/";

        public ProductCatalogServiceAPI()
        {
            products = new HttpClient();
            productsBaseUrl = Environment.GetEnvironmentVariable("ProductCatalogServiceURL");
            products.BaseAddress = new Uri(productsBaseUrl);
            responseMessage = null;
        }

        public virtual ProductDTO GetById(string id)
        {
            responseMessage = products.GetAsync(PRODUCTS_PRODUCT_ID + id);
            responseMessage.Wait();
            var r = responseMessage.Result.Content.ReadAsAsync<ProductDTO>().Result;
            if(responseMessage.Result.StatusCode == System.Net.HttpStatusCode.OK)
                return r;
            return new ProductDTO();
        }

        public virtual PageDTO GetPage(int? pageNumber, string name)
        {
            if (pageNumber.HasValue)
            {
                responseMessage = products.GetAsync(PRODUCTS_PAGE + pageNumber.Value);
                responseMessage.Wait();
                return responseMessage.Result.Content.ReadAsAsync<PageDTO>().Result;
            }
            responseMessage = products.GetAsync(PRODUCTS_NAME + name);
            responseMessage.Wait();
            var r = responseMessage.Result.Content.ReadAsAsync<PageDTO>().Result;
            if (responseMessage.Result.StatusCode == System.Net.HttpStatusCode.OK)
                return r;
            else
                return new PageDTO();
        }
    }
}
