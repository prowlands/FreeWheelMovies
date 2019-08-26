using FreeWheelMovies.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Shouldly;
using System.Net;
using Microsoft.EntityFrameworkCore;
using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Api.Test.TestData;
using FreeWheelMovies.Api.Models;

namespace FreeWheelMovies.Api.Test
{
    [TestClass]
    public class MovieRatingsControllerTests
    {
        private MovieRatingsController _controller;
        private MoviesContext _context;

        public void SetUpWithRatings()
        {
            _controller = null;
            _context = null;
            var options = new DbContextOptionsBuilder<MoviesContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            _context = new MoviesContext(options);
            MovieRatingsControllerTestDbInitialiser.Initialise(_context);

            _controller = new MovieRatingsController(_context);
        }

        public void SetUpWithOutRatings()
        {
            _controller = null;
            _context = null;
            var options = new DbContextOptionsBuilder<MoviesContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            _context = new MoviesContext(options);
            MovieRatingsControllerTestDbInitialiser.InitialiseWithNoRatings(_context);

            _controller = new MovieRatingsController(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            _controller = null;
            _context = null;
        }


        [TestMethod]
        public void GetShouldReturnNotFoundIfNoMoviesFound()
        {
            //ARRANGE            
            SetUpWithOutRatings();

            //ACT
            var result = _controller.Get();
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [TestMethod]
        public void GetShouldReturnOkIfMoviesFound() { }

        [TestMethod]
        public void GetByUserShouldReturnOkIfMoviesFound() { }

        [TestMethod]
        public void GetByUserShouldReturnNotFoundIfUserDoesntExist() { }

        [TestMethod]
        public void GetByUserShouldReturnNotFoundIfNoRatingsFound() { }

        [TestMethod]
        public void PostShouldReturnOkIfSaveSucceeds() {
            //ARRANGE     
            SetUpWithRatings();
            var request = new MovieRating
            {
                MovieId = 1,
                UserId = 1,
                Rating = 2
            };

            //ACT
            var result = _controller.Post(request);
            var resultStatus = result as OkObjectResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(200);
        }

        [TestMethod]
        public void PostShouldReturnNotFoundIfUserDoesntExist() {
            //ARRANGE            
            SetUpWithRatings();
            var request = new MovieRating
            {
                MovieId = _context.Movies.SingleAsync(x => x.Title.Contains("Shawshank")).Id,
                UserId = 100,
                Rating = 2
            };

            //ACT
            var result = _controller.Post(request);
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [TestMethod]
        public void PostShouldReturnNotFoundIfMovieDoesntExist() {
            //ARRANGE            
            SetUpWithRatings();
            var request = new MovieRating
            {
                MovieId = 100,
                UserId = _context.Users.SingleAsync(x => x.UserName == "Sarah").Id,
                Rating = 2
            };

            //ACT
            var result = _controller.Post(request);
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [TestMethod]
        public void PostShouldReturnBadRequestIfRatingIsTooLarge() {
            //ARRANGE            
            SetUpWithRatings();
            var request = new MovieRating
            {
                MovieId = _context.Movies.SingleAsync(x => x.Title.Contains("Shawshank")).Id,
                UserId = _context.Users.SingleAsync(x => x.UserName == "Sarah").Id,
                Rating = 6
            };

            //ACT
            var result = _controller.Post(request);
            var resultStatus = result as BadRequestResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(400);
        }

        [TestMethod]
        public void PostShouldReturnBadRequestIfRatingIsTooSmall()
        {
            //ARRANGE            
            SetUpWithRatings();
            var request = new MovieRating
            {
                MovieId = _context.Movies.SingleAsync(x => x.Title.Contains("Shawshank")).Id,
                UserId = _context.Users.SingleAsync(x => x.UserName == "Sarah").Id,
                Rating = 0
            };

            //ACT
            var result = _controller.Post(request);
            var resultStatus = result as BadRequestResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(400);
        }
    }
}
