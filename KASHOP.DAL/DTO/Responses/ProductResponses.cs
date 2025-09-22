using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace KASHOP.DAL.DTO.Responses
{
    public class ProductResponses
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }

       
        public string MainImageUrl { get; set; }
        public List<string> SubImageUrls { get; set; }=new List<string>();

        public List<ReviewResponses> Reviews { get; set; }= new List<ReviewResponses> { };
    }
}
