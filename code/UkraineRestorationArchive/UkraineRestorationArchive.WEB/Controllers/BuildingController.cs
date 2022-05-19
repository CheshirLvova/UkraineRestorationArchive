using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UkraineRestorationArchive.BLL;
using UkraineRestorationArchive.WEB.Models;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace UkraineRestorationArchive.WEB.Controllers
{

    public class BuildingController : Controller
    {
        private readonly IImagesManager _imagesManager;
        private readonly ILogger<BuildingController> _logger;
        private static string BAddress;
        private static List<byte[]> photos;
        public BuildingController(ILogger<BuildingController> logger, IImagesManager imagesManager)
        {
            _imagesManager = imagesManager;
            _logger = logger;
            
        }
        public IActionResult Index(string address)
        {
            photos = _imagesManager.getImages(address);

            BAddress = address;

            return View(photos);
        }
        public ActionResult GetImage(int Id)
        {
            byte[] image = photos[Id];
            return File(image, "image/png");
        }
        [HttpPost]
        public ActionResult AddImage(IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        byte[] buffer = new byte[file.Length];
                        var resultInBytes = ConvertToBytes(file);
                        Array.Copy(resultInBytes, buffer, resultInBytes.Length);

                        _imagesManager.addImage(User.Identity.Name, BAddress, buffer);
                    }
                }
            }
            return RedirectToAction("Index",new {address=BAddress });
        }
        private byte[] ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
