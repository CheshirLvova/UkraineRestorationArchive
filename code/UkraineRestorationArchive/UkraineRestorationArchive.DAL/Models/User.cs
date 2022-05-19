using System;
using System.Collections.Generic;

#nullable disable

namespace UkraineRestorationArchive.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddres { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
