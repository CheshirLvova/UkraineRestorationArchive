using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkraineRestorationArchive.BLL
{
    public interface IImagesManager
    {
        public void addImage(string username, string address, byte[] image);
        public List<byte[]> getImages(string address);
    }
}
