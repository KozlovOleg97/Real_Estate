﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Real_Estate.Core.Application.Helpers
{
    public class UploadImagesHelper
    {
        public static string UploadUserImage(IFormFile file, string userName)
        {
            // Get directory path

            string basePath = $"/Images/Users/{userName}";
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                $"wwwroot{basePath}");

            // Create folder if not exists

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Get file path

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{basePath}/{fileName}";
        }

        public static string UploadAgentUserImage(IFormFile file, string userName, bool isEditMode = false,
            string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }

            string basePath = $"/Images/Users/{userName}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{fileName}";
        }
        public static string UploadPropertyImage(IFormFile file, int id, bool isEditMode = false, 
            string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Properties/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();

            FileInfo fileInfo = new(file.FileName);

            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
