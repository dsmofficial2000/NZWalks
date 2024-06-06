﻿using NZWalks.API.Models.Domain;
using NZWalks.Data;

namespace NZWalks.API.Models.DTO
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NzWalksDBContext dBContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NzWalksDBContext dBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dBContext = dBContext;
        }


        public async Task<Image> Upload(Image image)
        {
            //var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", image.FileName);
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}"; 
            
            image.FilePath = urlFilePath;

            //add image to image table
            await dBContext.images.AddAsync(image);
            await dBContext.SaveChangesAsync();

            return image;

        }
    }
}
