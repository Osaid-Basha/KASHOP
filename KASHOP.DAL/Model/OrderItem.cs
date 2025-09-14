using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Model
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderItem
    {
        public int OrderId {  get; set; }
        public Order Order { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public decimal totalPrice { get; set; }
        public int Count {  get; set; }
        public decimal Price {  get; set; }
      
    }
}
