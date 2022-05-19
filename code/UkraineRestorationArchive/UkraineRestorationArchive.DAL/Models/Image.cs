using System;
using System.Collections.Generic;

#nullable disable

namespace UkraineRestorationArchive.DAL.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Image1 { get; set; }
        public string Address { get; set; }

        public virtual User UsernameNavigation { get; set; }
    }
}
