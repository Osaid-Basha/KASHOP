using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Model
{
   public enum Status
    {
        Active=1,
        Inactive=2,
    }
   public class BaseModel
    {
        public int Id {  get; set; }
        public  DateTime CreatedAt {  get; set; }
        public Status Status { get; set; }
    }
}
