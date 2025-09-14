using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Repositories.Interfaces;
using Stripe.Checkout;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHOP.BLL.Services.Class
{
    public class CheckOutServices : ICheckOutServices
    {
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IEmailSender emailSender;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IProductRepository productRepository;

        public CheckOutServices(
            ICartRepository cartRepository
            ,IOrderRepository orderRepository
            , IEmailSender emailSender
            ,IOrderItemRepository orderItemRepository
            , IProductRepository productRepository
            )
        {
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
            this.emailSender = emailSender;
            this.orderItemRepository = orderItemRepository;
            this.productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await orderRepository.GetUserByOrderAsync(orderId);

            var subject = "";
            var body = "";
            if (order.paymentMethod == PaymentMethodEnum.Visa)
            {
                order.status = OrderStatus.Approved;
                var carts = await cartRepository.getUserCartAsync(order.UserId);
                var orderItems=new List<OrderItem>();
                var productsUpdated = new List<(int productId, int quantity)>();
                foreach (var cartItem in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = orderId,
                        ProductId = cartItem.ProductId,
                        totalPrice = cartItem.Product.Price * cartItem.Count,
                        Count = cartItem.Count,
                        Price=cartItem.Product.Price,
                    };
                    orderItems.Add(orderItem);
                    productsUpdated.Add((cartItem.ProductId,cartItem.Count));

                }
                await orderItemRepository.AddRangeAsync(orderItems);
                await cartRepository.ClearCartAsync(order.UserId);
                await productRepository.DecreaseQuantityAsync(productsUpdated);
                subject = "Payment Successful - Kashop";

                body = $@"  
        <!DOCTYPE html>  
        <html>  
        <head>  
         <meta charset='UTF-8'>  
         <style>  
           body {{  
             font-family: Arial, sans-serif;  
             background-color: #f4f4f4;  
             margin: 0;  
             padding: 0;  
           }}  
           .container {{  
             max-width: 600px;  
             margin: 20px auto;  
             background: #ffffff;  
             border-radius: 8px;  
             box-shadow: 0 2px 6px rgba(0,0,0,0.1);  
             overflow: hidden;  
           }}  
           .header {{  
             background: linear-gradient(90deg,#4f46e5,#6366f1);  
             color: #fff;  
             text-align: center;  
             padding: 20px;  
             font-size: 20px;  
             font-weight: bold;  
           }}  
           .content {{  
             padding: 20px;  
             color: #333;  
             line-height: 1.6;  
           }}  
           .order-details {{  
             background: #f9fafb;  
             border: 1px solid #e5e7eb;  
             border-radius: 6px;  
             padding: 15px;  
             margin-top: 15px;  
           }}  
           .order-details p {{  
             margin: 6px 0;  
           }}  
           .footer {{  
             text-align: center;  
             font-size: 13px;  
             color: #666;  
             padding: 15px;  
             border-top: 1px solid #e5e7eb;  
             background: #fafafa;  
           }}  
         </style>  
        </head>  
        <body>  
         <div class='container'>  
           <div class='header'>  
             Kashop - Payment Confirmation  
           </div>  
           <div class='content'>  
             <p>Dear {order.User.UserName},</p>  
             <p>We are pleased to inform you that your payment has been   
                <strong style='color:#16a34a;'>successfully processed</strong> via <b>Visa</b>.</p>  

             <div class='order-details'>  
               <p><b>📌 Order Number:</b> {order.Id}</p>  
               <p><b>💰 Total Amount:</b> {order.TotelAmount:C}</p>  
               <p><b>📅 Payment Date:</b> {order.OrderDate}</p>  
             </div>  

             <p>Your order is now being prepared and will be shipped soon.<br/>  
                You can track your order status directly from your Kashop account.</p>  

             <p>Thank you for shopping with us!<br/>  
                <b>Kashop Team</b></p>  
           </div>  
           <div class='footer'>  
             &copy; {DateTime.Now.Year} Kashop. All rights reserved.  
           </div>  
         </div>  
        </body>  
        </html>  
        ";
            }
            else if (order.paymentMethod == PaymentMethodEnum.Cash)
            {
                subject = "Payment Successful - Kashop";

                body = $@"  
        <!DOCTYPE html>  
        <html>  
        <head>  
         <meta charset='UTF-8'>  
         <style>  
           body {{  
             font-family: Arial, sans-serif;  
             background-color: #f4f4f4;  
             margin: 0;  
             padding: 0;  
           }}  
           .container {{  
             max-width: 600px;  
             margin: 20px auto;  
             background: #ffffff;  
             border-radius: 8px;  
             box-shadow: 0 2px 6px rgba(0,0,0,0.1);  
             overflow: hidden;  
           }}  
           .header {{  
             background: linear-gradient(90deg,#4f46e5,#6366f1);  
             color: #fff;  
             text-align: center;  
             padding: 20px;  
             font-size: 20px;  
             font-weight: bold;  
           }}  
           .content {{  
             padding: 20px;  
             color: #333;  
             line-height: 1.6;  
           }}  
           .order-details {{  
             background: #f9fafb;  
             border: 1px solid #e5e7eb;  
             border-radius: 6px;  
             padding: 15px;  
             margin-top: 15px;  
           }}  
           .order-details p {{  
             margin: 6px 0;  
           }}  
           .footer {{  
             text-align: center;  
             font-size: 13px;  
             color: #666;  
             padding: 15px;  
             border-top: 1px solid #e5e7eb;  
             background: #fafafa;  
           }}  
         </style>  
        </head>  
        <body>  
         <div class='container'>  
           <div class='header'>  
             Kashop - Payment Confirmation  
           </div>  
           <div class='content'>  
             <p>Dear {order.User.UserName},</p>  
             <p>We are pleased to inform you that your payment has been   
                <strong style='color:#16a34a;'>successfully processed</strong> via <b>Cash</b>.</p>  

             <div class='order-details'>  
               <p><b>📌 Order Number:</b> {order.Id}</p>  
               <p><b>💰 Total Amount:</b> {order.TotelAmount:C}</p>  
               <p><b>📅 Payment Date:</b> {order.OrderDate}</p>  
             </div>  

             <p>Your order is now being prepared and will be shipped soon.<br/>  
                You can track your order status directly from your Kashop account.</p>  

             <p>Thank you for shopping with us!<br/>  
                <b>Kashop Team</b></p>  
           </div>  
           <div class='footer'>  
             &copy; {DateTime.Now.Year} Kashop. All rights reserved.  
           </div>  
         </div>  
        </body>  
        </html>  
        ";
            }

            await emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        public async Task<CheckOutResponses> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest Request)
        {
            var cartItems = await cartRepository.getUserCartAsync(UserId); // Await the task to get the result  
            if (!cartItems.Any()) // Use the result to check for items  
            {
                return new CheckOutResponses
                {
                    Success = false,
                    Massage = "Cart is empty."
                };
            }
            Order order = new Order
            {
                UserId = UserId,
                paymentMethod = request.PaymentMethod,
                TotelAmount = cartItems.Sum(ci => ci.Product.Price * ci.Count) // Use the awaited result here  
            };
            await orderRepository.addAsync(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                await Task.CompletedTask;
                return new CheckOutResponses
                {
                    Success = true,
                    Massage = "Payment processed successfully with Cash."
                };
            }

            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOuts/Success/{order.Id}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOuts/Cancel",
                };

                foreach (var item in cartItems) // Use the awaited result here  
                {
                    options.LineItems.Add(
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "USD",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Name,
                                    Description = item.Product.Description
                                },
                                UnitAmount = (long)item.Product.Price,
                            },
                            Quantity = item.Count,
                        });
                }

                var service = new SessionService();
                var session = service.Create(options);
                order.PaymentId = session.Id;
                return new CheckOutResponses
                {
                    Success = true,
                    Massage = "Payment session created successfully",
                    Url = session.Url,
                    PaymentId = session.Id,
                };
            }

            return new CheckOutResponses
            {
                Success = false,
                Massage = "Invalid payment method."
            };
        }
    }
}
