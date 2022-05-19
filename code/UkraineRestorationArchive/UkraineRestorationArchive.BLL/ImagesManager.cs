using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkraineRestorationArchive.DAL.Models;

namespace UkraineRestorationArchive.BLL
{
    public class ImagesManager:IImagesManager
    {
        private UkraineRestorationArchiveDBContext _db;
        public ImagesManager(UkraineRestorationArchiveDBContext dBContext)
        {
            _db = dBContext;
        }
        public void addImage(string username,string address,byte[] image)
        {
            _db.Images.Add(new Image { Address = address, Username = username, Image1 = image });
            _db.SaveChanges();
        }
        public List<byte[]> getImages(string address)
        {
            var res=from a in _db.Images where a.Address == address select a.Image1;
            List<byte[]> rs = res.ToList();
            return rs;

        }
    }
}
