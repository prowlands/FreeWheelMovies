using System;
using System.Collections.Generic;
using System.Text;

namespace FreeWheelMovies.Database.Models
{
    public class MovieGenre
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public int MovieId { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
