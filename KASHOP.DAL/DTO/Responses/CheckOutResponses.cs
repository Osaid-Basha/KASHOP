﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
    public class CheckOutResponses
    {
        public bool Success {  get; set; }
        public string Massage { get; set; }
        public string? Url { get; set; }
        public string ? PaymentId {  get; set; }
    }
}
