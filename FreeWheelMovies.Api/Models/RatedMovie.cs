using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWheelMovies.Api.Models
{
    public class RatedMovie
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime yearOfRelease { get; set; }
        public int runningTime { get; set; }
        public double averageRating { get; set; }
    }
}
