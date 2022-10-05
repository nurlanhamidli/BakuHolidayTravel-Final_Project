using bht_demo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;

namespace bht_demo.Extentions
{
    public static class Extention
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
        public static bool IsImgSize(this IFormFile file, int b)
        {
            return file.Length > b;
        }
        public static async Task<string> SaveImage(this IFormFile file, IWebHostEnvironment env, string folder)
        {
            string path = env.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string result = Path.Combine(path, folder, fileName);
            using (FileStream stream = new FileStream(result, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
