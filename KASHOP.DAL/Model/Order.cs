using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Model
{
    public enum OrderStatus
    {
        Pending=1,
        Canceled=2,
        Approved=3,
        Shipped=4,
        Delivered=5,
        Success=6,
    }
    public enum PaymentMethodEnum
    {
        Cash=1,Visa=2
    }
    public class Order
    {
        //order
        public int Id { get; set; }
        public OrderStatus status { get; set; }= OrderStatus.Pending;
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public DateTime ShippedDate {  get; set; }
        public decimal TotelAmount { get; set; }
        //payment
        public PaymentMethodEnum paymentMethod { get; set; }
        public string? PaymentId { get; set; }
        //carrier
        public string? CarrierName { get; set; }
        public string? CarrierNumber { get;set; }
        //relation
        public string UserId {  get; set; }
        public ApplicationUser User { get; set; }

        
    }
}
