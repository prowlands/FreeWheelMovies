using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeWheelMovies.Api.Test.TestData
{
    public static class MovieRatingsControllerTestDbInitialiser
    {
        //Class to initialise the dataset
        public static void Initialise(MoviesContext context)
        {
            //Checks to see if any movies have been added, if they have then the database has already been initialised and we should return
            if (context.Movies.Any())
            {
                return;
            }

            AddUsers(context);
            AddMovies(context);
            AddRatings(context);
        }

        public static void InitialiseWithNoRatings(MoviesContext context)
        {
            //Checks to see if any movies have been added, if they have then the database has already been initialised and we should return
            if (context.Movies.Any())
            {
                return;
            }

            AddUsers(context);
            AddMovies(context);
        }


        private static void AddUsers(MoviesContext context)
        {
            var usersToAdd = new User[]
            {
                new User { Id = 1, UserName = "Tony" },
                new User { Id = 2, UserName = "Sarah" },
                new User { Id = 3, UserName = "Anna" },
                new User { Id = 4, UserName = "Bob" },
                new User { Id = 5, UserName = "Alice" },
                new User { Id = 6, UserName = "John" },
            };

            foreach (User u in usersToAdd)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        private static void AddMovies(MoviesContext context)
        {
            var moviesToAdd = new Movie[]
            {
                new Movie
                {
                    Id = 1,
                    Title = "The Shawshank Redemption",
                    ReleaseYear = new DateTime(1994,1,1),
                    RunningTime = 142
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Godfather",
                    ReleaseYear = new DateTime(1972,1,1),
                    RunningTime = 175
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Godfather - Part II",
                    ReleaseYear = new DateTime(1974,1,1),
                    RunningTime = 202
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Dark Knight",
                    ReleaseYear = new DateTime(2008,1,1),
                    RunningTime = 152
                },
                new Movie
                {
                    Id = 5,
                    Title = "12 Angry Men",
                    ReleaseYear = new DateTime(1957,1,1),
                    RunningTime = 96
                }
            };

            foreach (Movie m in moviesToAdd)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();
        }


        private static void AddRatings(MoviesContext context)
        {
            var shawshankId = context.Movies.Single(x => x.Title == "The Shawshank Redemption").Id;
            var godfatherId = context.Movies.Single(x => x.Title == "The Godfather").Id;
            var godfather2Id = context.Movies.Single(x => x.Title == "The Godfather - Part II").Id;
            var darkKnightId = context.Movies.Single(x => x.Title == "The Dark Knight").Id;
            var angryMenId = context.Movies.Single(x => x.Title == "12 Angry Men").Id;
            var tonyId = context.Users.Single(x => x.UserName == "Tony").Id;
            var sarahId = context.Users.Single(x => x.UserName == "Sarah").Id;
            var annaId = context.Users.Single(x => x.UserName == "Anna").Id;
            var bobId = context.Users.Single(x => x.UserName == "Bob").Id;
            var aliceId = context.Users.Single(x => x.UserName == "Alice").Id;
            var johnId = context.Users.Single(x => x.UserName == "John").Id;

            var ratingsToAdd = new Rating[]
            {
                new Rating
                {
                    MovieId = godfatherId,
                    UserId = tonyId,
                    UserRating = 4
                },
                new Rating
                {
                    MovieId = godfatherId,
                    UserId = aliceId,
                    UserRating = 5
                },
                new Rating
                {
                    MovieId = godfatherId,
                    UserId = johnId,
                    UserRating = 2
                },
                new Rating
                {
                    MovieId = shawshankId,
                    UserId = tonyId,
                    UserRating = 5
                },
                new Rating
                {
                    MovieId = shawshankId,
                    UserId = johnId,
                    UserRating = 4
                },
                new Rating
                {
                    MovieId = darkKnightId,
                    UserId = annaId,
                    UserRating = 5
                },
                new Rating
                {
                    MovieId = darkKnightId,
                    UserId = sarahId,
                    UserRating = 4
                },
                new Rating
                {
                    MovieId = godfatherId,
                    UserId = sarahId,
                    UserRating = 5
                },
            };

            foreach (Rating r in ratingsToAdd)
            {
                context.Ratings.Add(r);
            }
            context.SaveChanges();
        }
    }
}
