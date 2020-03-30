using SerializeObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationService.Models
{
    public class ProductCatalogServiceAPIMock : ProductCatalogServiceAPI
    {
        private const string FILE_NAME = "DataProductsMock.xml";
        private const string FILE_PATH = @"C:\Users\Curso\source\repos\TeamUno\";
        List<ProductDTO> productDTOs;

        public ProductCatalogServiceAPIMock()
        {
            XMLFile.Path = FILE_PATH;
            productDTOs = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);
        }

        public override ProductDTO GetById(string id)
        {
            var product = productDTOs.FirstOrDefault(p => p.Id == id);
            return product != null ? product : new ProductDTO();
        }

        public override PageDTO GetPage(int? pageNumber, string name)
        {
            return new PageDTO { TotalItems = productDTOs.Count, Products = productDTOs };
        }
    }
}
