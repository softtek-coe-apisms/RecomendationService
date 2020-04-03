using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationService.Models
{
    public class PageDTO
    {
        public List<ProductDTO> Products { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages { get; set; }
    }
}
