﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Model
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price {  get; set; }
        public decimal Quantity { get; set; }
        public string MainImage {  get; set; }
        public double Rate {  get; set; }

        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        public int? BrandId {  get; set; }
        public Brand? Brand { get; set; }


        public List<ProductImage> SubImages { get; set; }=new List<ProductImage>();

    }
}
