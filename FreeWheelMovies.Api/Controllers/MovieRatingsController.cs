using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeWheelMovies.Api.Models;
using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeWheelMovies.Api.Controllers
{
    [Route("api/[controller]")]
    public class MovieRatingsController : Controller
    {
        private readonly MoviesContext _context;

        public MovieRatingsController(MoviesContext context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet()]
        public IActionResult Get()
        {
            var resultSet = _context.Movies.OrderByDescending(x => x.Ratings.Average(y => y.UserRating)).ThenBy(x => x.Title).Take(5);

            var returnedItems = resultSet.Select(x => new
            {
                id = x.Id,
                title = x.Title,
                yearOfRelease = x.ReleaseYear.Year,
                runningTime = x.RunningTime,
                averageRating = x.Ratings.Average(y => y.UserRating)
            });

            return Ok(returnedItems);

        }

        // GET api/values/5
        [HttpGet("userId")]
        public IActionResult Get(int userId)
        {
            if(userId == 0){
                return BadRequest();
            }

            var resultSet = _context.Ratings.Where(x => x.UserId == userId).OrderByDescending(y => y.UserRating).ThenBy(x => x.Movie.Title).Take(5).Select( x => x.MovieId).ToList();

            if(resultSet.Count == 0)
            {
                return NotFound();
            }

            var movieList = _context.Movies.Where(x => resultSet.Contains(x.Id));

            var returnedItems = movieList.Select(x => new
            {
                id = x.Id,
                title = x.Title,
                yearOfRelease = x.ReleaseYear.Year,
                runningTime = x.RunningTime,
                averageRating = x.Ratings.Average(y => y.UserRating)
            });

            return Ok(returnedItems);
        }

        [HttpPost]
        public IActionResult Post([FromBody]MovieRating movieRating)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == movieRating.UserId);
            var movie = _context.Movies.SingleOrDefault(x => x.Id == movieRating.MovieId);
            if(user == default(User) || movie == default(Movie))
            {
                return NotFound();
            }

            if(movieRating.Rating < 1 || movieRating.Rating > 5)
            {
                return BadRequest();
            }


            var existingRating = _context.Ratings.SingleOrDefault(x => x.UserId == movieRating.UserId && x.MovieId == movieRating.MovieId);

            if (existingRating == default(Rating))
            {
                _context.Ratings.Add(new Rating
                {
                    UserId = movieRating.UserId,
                    MovieId = movieRating.MovieId,
                    UserRating = movieRating.Rating
                });
            } else
            {
                //Technically this is a breach of ReST, it should be a separate PUT call however this approach makes it easier for the calling service.
                existingRating.UserRating = movieRating.Rating;
                _context.Ratings.Update(existingRating);
            }

            if(_context.SaveChanges() == 1)
            {
                return Ok();
            }

            //If we have got this far, the update has failed so we should return an error
            return StatusCode(StatusCodes.Status500InternalServerError);


        }

    }
}
