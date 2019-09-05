
using FreeWheelMovies.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

using System;
using Shouldly;
using System.Net;
using Microsoft.EntityFrameworkCore;
using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Api.Test.TestData;
using NUnit.Framework;

namespace FreeWheelMovies.Api.Test
{
    [TestFixture]
    public class MoviesControllerTests
    {
        private MoviesController _controller;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var context = new MoviesContext(options);
            MoviesControllerTestDbInitialiser.Initialise(context);

            _controller = new MoviesController(context);
        }


        [Test]
        public void GetShouldReturnNotFoundIfNoMoviesFound()
        {
            //ARRANGE            
            var searchTitle = "not found title";

            //ACT
            var result = _controller.Get(searchTitle, DateTime.Now, new System.Collections.Generic.List<string>());
            var resultStatus = result as NotFoundResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(404);
        }

        [Test]
        public void GetShouldReturnBadRequestForInvalidRequest()
        {
            //ARRANGE

            //ACT
            var result = _controller.Get(string.Empty, null, new System.Collections.Generic.List<string>());
            //Cast to expected to result model to check the correct status code has been returned
            var resultStatus = result as BadRequestResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(400);
        }

        [Test]
        public void GetShouldReturnOkForValidRequest()
        {
            //ARRANGE            
            var searchTitle = "Shawshank";

            //ACT
            var result = _controller.Get(searchTitle, null, new System.Collections.Generic.List<string>());
            var resultStatus = result as OkObjectResult;

            //ASSERT
            resultStatus.StatusCode.ShouldBe(200);            
        }
    }
}
