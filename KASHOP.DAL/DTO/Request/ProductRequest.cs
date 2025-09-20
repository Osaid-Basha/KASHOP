using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public IFormFile MainImage { get; set; }
        public List<IFormFile> SubImage { get; set; }
        public double Rate { get; set; }

        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
