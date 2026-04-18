using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CloudinaryImageService: IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageService(IOptions<CloudinarySettings> settings)
        {
            var cloudinarySettings = settings.Value;
            _cloudinary = new Cloudinary(new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.SecretKey
            ));
        }

        public async Task DeleteAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            if (deletionResult.Error != null)
            {
                throw new Exception($"Image deletion failed: {deletionResult.Error.Message}");
            }
        }

        public async Task<(string url,string publicId)> UploadImageAsync(IFormFile file,int userId)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.");
            }

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
               // Folder = $"products/user_{userId}",
                //Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };


            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if(uploadResult.Error != null)
            {
                throw new Exception($"Image upload failed: {uploadResult.Error.Message}");
            }
            return ( uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }
    }
}
