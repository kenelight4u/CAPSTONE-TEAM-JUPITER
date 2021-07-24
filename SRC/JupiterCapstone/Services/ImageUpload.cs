using System;
using System.Collections.Generic;
using System.Linq;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using JupiterCapstone.Services.IService;

namespace JupiterCapstone.Services
{
    public class ImageUpload:IImageService
    {
        private  string ApiKey { get; set; }
        private  string ApiSecret { get; set; }
        private  string Cloud { get; set; }
            
        public ImageUpload(IConfiguration configuration)
        {
            ApiKey = configuration["Cloudinary:ApiKey"];
            ApiSecret = configuration["Cloudinary:ApiSecret"];
            Cloud = configuration["Cloudinary:Cloud"];

        }
        public string UploadImage(string filePath)
        {
            Account account = new Account()
            {
                ApiKey = ApiKey,
                ApiSecret = ApiSecret,
                Cloud = Cloud,
                
            };

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                Folder = "AduabaProduct",
                Invalidate = true
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }

    }
}
