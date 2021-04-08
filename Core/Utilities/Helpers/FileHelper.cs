using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Constants;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        public static string CreateNewPath(IFormFile file)
        {
            string uniqueFileName = null;
            string fileExtension = Path.GetExtension(file.FileName);
            uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", uniqueFileName);
            return filePath;
        }

        public static IDataResult<string> AddFile(IFormFile file)
        {
            string filePath = null;

            if (file.Length > 0)
            {
                if (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/jpg")
                {
                    filePath = CreateNewPath(file);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return new SuccessDataResult<string>(filePath, Messages.FileAdded);
                }
            }
            return new ErrorDataResult<string>(Messages.WrongFileType);
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                if (path.Contains("default.png") == false)
                {
                    File.Delete(path);
                }
            }
        }

        public static IDataResult<string> UpdateFile(string path, IFormFile file)
        {
            string filePath = null;
            DeleteFile(path);

            if (file.Length > 0)
            {
                if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png")
                {
                    filePath = CreateNewPath(file);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return new SuccessDataResult<string>(filePath, Messages.FileUpdated);
                }
            }
            return new ErrorDataResult<string>(Messages.WrongFileType);
        }
    }
}
