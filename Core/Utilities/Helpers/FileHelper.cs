using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;


namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var newPath = CreateNewPath(file);
            if (file?.Length>0)
            {
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
                
            
            var separator = new string[] { "wwwroot" };
            var relativePath = newPath.Split(separator, StringSplitOptions.None)[1];
            return relativePath;

        }
        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();

        }
        public static string Update(string sourcePath, IFormFile file)
        {
            string path = Environment.CurrentDirectory + @"\wwwroot";
            var newPath = CreateNewPath(file);

            if (sourcePath.Length > 0)
            {
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            File.Delete(path+sourcePath);
            var separator = new string[] { "wwwroot" };
            var relativePath = newPath.Split(separator, StringSplitOptions.None)[1];
            return relativePath;
        }

        public static string CreateNewPath(IFormFile file)
        {
            
            if (file==null)
            {
                return Environment.CurrentDirectory + @"\wwwroot\null.jpg";
            }
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileExtension = fileInfo.Extension;
                var newFileName = Guid.NewGuid().ToString() + fileExtension;

                string path = Environment.CurrentDirectory + @"\wwwroot\Images";
                string result = $@"{path}\{newFileName}";
                return result;
            
        }
    }
}
