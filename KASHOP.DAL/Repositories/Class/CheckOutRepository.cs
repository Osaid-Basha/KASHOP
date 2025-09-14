using KASHOP.DAL.Data;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Class
{
    public class CheckOutRepository : ICheckOutRepository
    {
        private readonly ApplicationDbContext context;

        public CheckOutRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

       
    }
}
