﻿using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class FileServices : IFileServices
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
           if(file != null && file.Length > 0)
            {
                var fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Images", fileName);
                using( var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            throw new Exception("erorr");
        }
        
    }
}
