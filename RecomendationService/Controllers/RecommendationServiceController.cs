using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecommendationService.Models;

namespace RecomendationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendationServiceController : ControllerBase
    {
        ProductCatalogServiceAPI productsAPI;

        public RecomendationServiceController(bool mock = false)
        {
            if (mock)
                productsAPI = new ProductCatalogServiceAPIMock();
            else
                productsAPI = new ProductCatalogServiceAPI();
        }

        [HttpGet]
        [Route("")]
        public ActionResult Ok200ServerWorking()
        {
            return Ok("Server Working");
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetRecomendation(string id)
        {
            try
            {
                //obtaint the information related with the product
                List<string> actualCategories = productsAPI.GetById(id).Categories;

                //obtaining total pages
                int totalPages = productsAPI.GetPage(1, "").TotalItems;

                //Looking for products with similar categories
                List<ProductDTO> recomendedProducts = new List<ProductDTO>();
                while (totalPages > 0)
                {
                    List<ProductDTO> actualPage = productsAPI.GetPage(totalPages--, "").Products;
                    List<ProductDTO> productsSameCategory = actualPage
                        .Select(p => new { likeness = Utilities.CompareStringLists(actualCategories, p.Categories), p })
                        .Where(an => an.p.Id != id && an.likeness > 0)
                        .OrderByDescending(an => an.likeness)
                        .Take(10)
                        .Select(an => an.p)
                        .ToList();
                    recomendedProducts.AddRange(productsSameCategory);
                }

                return Ok(recomendedProducts);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}