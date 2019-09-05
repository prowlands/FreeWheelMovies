using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeWheelMovies.Api.Models;
using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreeWheelMovies.Api.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly MoviesContext _context;

        public MoviesController(MoviesContext context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet()]
        public IActionResult Get(string title, DateTime? releaseYear, List<string> genre)
        {
            if(string.IsNullOrWhiteSpace(title) && releaseYear == null && genre?.Count == 0){
                return BadRequest();
            }


            List<Movie> resultSet = new List<Movie>();
            resultSet = searchDatabase(title, releaseYear, genre);
            
            if(resultSet.Count == 0)
            {
                return NotFound();
            }

            var returnedItems = resultSet.Select(x => new RatedMovie
            {
                id = x.Id,
                title = x.Title,
                yearOfRelease = x.ReleaseYear,
                runningTime = x.RunningTime,
                averageRating = _context.Ratings.Where(y => y.MovieId == x.Id).Average(y => y.UserRating).RoundToNearestHalf()
            });

            return Ok(returnedItems);
        }

        private List<Movie> searchDatabase(string title, DateTime? releaseYear, List<string> genre)
        {
            List<Movie> resultSet = new List<Movie>();
            if (!string.IsNullOrWhiteSpace(title))
            {
                resultSet.AddRange(_context.Movies.Where(x => x.Title.ToLower().Contains(title.ToLower())));
            }
            if (releaseYear.HasValue)
            {
                if (resultSet.Any())
                {
                    resultSet = resultSet.Where(x => x.ReleaseYear.Year == releaseYear.Value.Year).ToList();
                }
                else
                {
                    resultSet.AddRange(_context.Movies.Where(x => x.ReleaseYear.Year == releaseYear.Value.Year));
                }
            }
            if (genre.Count > 0)
            {
                if (resultSet.Any())
                {
                    resultSet = resultSet.Where(x => x.Genres.Select(y => y.Genre.GenreName).ToList().Intersect(genre).Any()).ToList();
                }
                else
                {
                    resultSet.AddRange(_context.Movies.Where(x => x.Genres.Select(y => y.Genre.GenreName).Intersect(genre).Any()));
                }
            }

            return resultSet;
        }

    }
}
