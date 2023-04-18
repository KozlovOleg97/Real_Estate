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
	}
}
