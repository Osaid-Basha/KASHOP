using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
    public class ReviewResponses
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public string FullName { get; set; }

    }
}
