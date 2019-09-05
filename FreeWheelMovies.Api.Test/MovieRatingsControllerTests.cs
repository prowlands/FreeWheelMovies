using FreeWheelMovies.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Shouldly;
using System.Net;
using Microsoft.EntityFrameworkCore;
using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Api.Test.TestData;
using FreeWheelMovies.Api.Models;
using NUnit.Framework;

namespace FreeWheelMovies.Api.Test
{
    [TestFixture]
    public class MovieRatingsControllerTests
    {   
        [Test]
        public void GetShouldReturnNotFoundIfNoMoviesFound()
        {
            //ARRANGE
            var controller = SetUpWithOutRatings();

            //ACT
            var result = controller.Get();
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [Test]
        public void GetShouldReturnOkIfMoviesFound() { }

        [Test]
        public void GetByUserShouldReturnOkIfMoviesFound() { }

        [Test]
        public void GetByUserShouldReturnNotFoundIfUserDoesntExist() { }

        [Test]
        public void GetByUserShouldReturnNotFoundIfNoRatingsFound() { }

        [Test]
        public void PostShouldReturnOkIfSaveSucceeds() {
            //ARRANGE   
            var controller = SetUpWithRatings();
            var request = new MovieRating
            {
                MovieId = 1,
                UserId = 1,
                Rating = 2
            };

            //ACT
            var result = controller.Post(request);
            var resultStatus = result as OkResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(200);
        }

        [Test]
        public void PostShouldReturnNotFoundIfUserDoesntExist() {
            //ARRANGE           
            var request = new MovieRating
            {
                MovieId = 1,
                UserId = 100,
                Rating = 2
            };
            var controller = SetUpWithRatings();

            //ACT
            var result = controller.Post(request);
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [Test]
        public void PostShouldReturnNotFoundIfMovieDoesntExist() {
            //ARRANGE          
            var request = new MovieRating
            {
                MovieId = 100,
                UserId = 2,
                Rating = 2
            };
            var controller = SetUpWithRatings();

            //ACT
            var result = controller.Post(request);
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [Test]
        public void PostShouldReturnBadRequestIfRatingIsTooLarge() {
            //ARRANGE           
            var request = new MovieRating
            {
                MovieId = 2,
                UserId = 1,
                Rating = 6
            };
            var controller = SetUpWithRatings();

            //ACT
            var result = controller.Post(request);
            var resultStatus = result as BadRequestResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(400);
        }

        [Test]
        public void PostShouldReturnBadRequestIfRatingIsTooSmall()
        {
            //ARRANGE          
            var request = new MovieRating
            {
                MovieId = 1,
                UserId = 1,
                Rating = 0
            };
            var controller = SetUpWithRatings();

            //ACT
            var result = controller.Post(request);
            var resultStatus = result as BadRequestResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(400);
        }


        public MovieRatingsController SetUpWithRatings()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var context = new MoviesContext(options);
            MovieRatingsControllerTestDbInitialiser.Initialise(context);

            return new MovieRatingsController(context);
        }

        public MovieRatingsController SetUpWithOutRatings()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var context = new MoviesContext(options);
            MovieRatingsControllerTestDbInitialiser.InitialiseWithNoRatings(context);

            return new MovieRatingsController(context);
        }
    }
}
