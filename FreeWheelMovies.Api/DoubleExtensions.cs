using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWheelMovies.Api
{
    public static  class DoubleExtensions
    {
        public static double RoundToNearestHalf(this double numberToRound)
        {
            //By doubling the number, then rounding it, we will take ourselves to twice the
            // .5 rounded value of our original number. 
            // e.g. 3.3 * 2 = 6.6, this rounds to 7. 7/2 is 3.5
            var doubledNumber = numberToRound * 2;
            return Math.Round(doubledNumber) / 2;

        }
    }

}
