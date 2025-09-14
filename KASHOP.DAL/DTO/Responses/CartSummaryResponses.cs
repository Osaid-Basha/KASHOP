using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
    public class CartSummaryResponses
    {
        public List<CartResponses> Item { get; set; }=new List<CartResponses>();

        public decimal CartTotle=>Item.Sum(i=>i.TotalPrice);
    }
}
