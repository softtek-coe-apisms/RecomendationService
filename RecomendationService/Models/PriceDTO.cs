using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationService.Models
{
    public class PriceDTO
    {
        public string CurrencyCode { get; set; }
        public int Units { get; set; }
        public int Nanos { get; set; }
    }
}
