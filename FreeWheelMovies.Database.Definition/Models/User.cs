using System;
using System.Collections.Generic;
using System.Text;

namespace FreeWheelMovies.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public ICollection<Rating> Ratings { get; set; }

    }
}
