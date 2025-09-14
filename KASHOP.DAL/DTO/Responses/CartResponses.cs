using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
    public class CartResponses
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice => Price * Count;


    }
}
