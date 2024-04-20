using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_Genel.ViewModels
{
    public class ProductsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Thumbnail { get; set; }
        public string[] Images { get; set; }
    }
}
