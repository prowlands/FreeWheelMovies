using System;
using System.Collections.Generic;
using System.Text;

namespace FreeWheelMovies.Database.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int RunningTime { get; set; }

        public ICollection<MovieGenre> Genres { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
