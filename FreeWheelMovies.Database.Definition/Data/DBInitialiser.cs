using FreeWheelMovies.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeWheelMovies.Database.Data
{
    public static class DBInitialiser
    {
        //Class to initialise the dataset
        public static void Initialise(MoviesContext context)
        {
            //Checks to see if any movies have been added, if they have then the database has already been initialised and we should return
            if (context.Movies.Any())
            {
                return;
            }

            var genresToAdd = new Genre[]
            {
                new Genre { GenreName = "Thriller"},
                new Genre { GenreName = "Comedy"},
                new Genre { GenreName = "SciFi"},
                new Genre { GenreName = "Action"},
                new Genre { GenreName = "Drama"},
                new Genre { GenreName = "History"},
                new Genre { GenreName = "Documentary"},
            };

            foreach (Genre m in genresToAdd)
            {
                context.Genres.Add(m);
            }
            context.SaveChanges();


            var usersToAdd = new User[]
            {
                new User { UserName = "Tony" },
                new User { UserName = "Sarah" },
                new User { UserName = "Anna" },
                new User { UserName = "Bob" },
                new User { UserName = "Alice" },
                new User { UserName = "John" },
            };

            foreach (User u in usersToAdd)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var moviesToAdd = new Movie[]
            {
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    ReleaseYear = new DateTime(1994,1,1),
                    RunningTime = 142
                },
                new Movie
                {
                    Title = "The Godfather",
                    ReleaseYear = new DateTime(1972,1,1),
                    RunningTime = 175
                },
                new Movie
                {
                    Title = "The Godfather - Part II",
                    ReleaseYear = new DateTime(1974,1,1),
                    RunningTime = 202
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseYear = new DateTime(2008,1,1),
                    RunningTime = 152
                },
                new Movie
                {
                    Title = "12 Angry Men",
                    ReleaseYear = new DateTime(1957,1,1),
                    RunningTime = 96
                },
                new Movie
                {
                    Title = "Schindler's List",
                    ReleaseYear = new DateTime(1993,1,1),
                    RunningTime = 195
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    ReleaseYear = new DateTime(2003,1,1),
                    RunningTime = 201
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    ReleaseYear = new DateTime(1994,1,1),
                    RunningTime = 154
                },
                new Movie
                {
                    Title = "The Good, the Bad and the Ugly",
                    ReleaseYear = new DateTime(1966,1,1),
                    RunningTime = 148
                },
                new Movie
                {
                    Title = "Fight Club",
                    ReleaseYear = new DateTime(1999,1,1),
                    RunningTime = 139
                },
            };

            foreach (Movie m in moviesToAdd)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var shawshankId = context.Movies.Single(x => x.Title == "The Shawshank Redemption").Id;
            var godfatherId = context.Movies.Single(x => x.Title == "The Godfather").Id;
            var godfather2Id = context.Movies.Single(x => x.Title == "The Godfather - Part II").Id;
            var darkKnightId = context.Movies.Single(x => x.Title == "The Dark Knight").Id;
            var angryMenId = context.Movies.Single(x => x.Title == "12 Angry Men").Id;
            var schindlerId = context.Movies.Single(x => x.Title == "Schindler's List").Id;
            var lotrId = context.Movies.Single(x => x.Title == "The Lord of the Rings: The Return of the King").Id;
            var pulpFictionId = context.Movies.Single(x => x.Title == "Pulp Fiction").Id;
            var goodBadUglyId = context.Movies.Single(x => x.Title == "The Good, the Bad and the Ugly").Id;
            var fighClubId = context.Movies.Single(x => x.Title == "Fight Club").Id;

            var tonyId = context.Users.Single(x => x.UserName == "Tony").Id;
            var sarahId = context.Users.Single(x => x.UserName == "Sarah").Id;
            var annaId = context.Users.Single(x => x.UserName == "Anna").Id;
            var bobId = context.Users.Single(x => x.UserName == "Bob").Id;
            var aliceId = context.Users.Single(x => x.UserName == "Alice").Id;
            var johnId = context.Users.Single(x => x.UserName == "John").Id;

            var thrillerId = context.Genres.Single(x => x.GenreName == "Thriller").Id;
            var comedyId = context.Genres.Single(x => x.GenreName == "Comedy").Id;
            var sciFiId = context.Genres.Single(x => x.GenreName == "SciFi").Id;
            var actionId = context.Genres.Single(x => x.GenreName == "Action").Id;
            var dramaId = context.Genres.Single(x => x.GenreName == "Drama").Id;
            var historyId = context.Genres.Single(x => x.GenreName == "History").Id;
            var docoId = context.Genres.Single(x => x.GenreName == "Documentary").Id;

            var movieGenresToAdd = new MovieGenre[]
            {
                new MovieGenre{ MovieId = shawshankId, GenreId = thrillerId },
                new MovieGenre{ MovieId = godfatherId, GenreId = thrillerId },
                new MovieGenre{ MovieId = godfatherId, GenreId = actionId },
                new MovieGenre{ MovieId = godfatherId, GenreId = dramaId },
                new MovieGenre{ MovieId = schindlerId, GenreId = historyId},
                new MovieGenre{ MovieId = schindlerId, GenreId = dramaId },
            };

            foreach (MovieGenre mg in movieGenresToAdd)
            {
                context.MovieGenres.Add(mg);
            }
            context.SaveChanges();


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
                    MovieId = fighClubId,
                    UserId = bobId,
                    UserRating = 3
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
                    MovieId = goodBadUglyId,
                    UserId = sarahId,
                    UserRating = 5
                },
                new Rating
                {
                    MovieId = pulpFictionId,
                    UserId = sarahId,
                    UserRating = 2
                },
                new Rating
                {
                    MovieId = pulpFictionId,
                    UserId = annaId,
                    UserRating = 4
                },
                new Rating
                {
                    MovieId = pulpFictionId,
                    UserId = bobId,
                    UserRating = 3
                },
                new Rating
                {
                    MovieId = goodBadUglyId,
                    UserId = bobId,
                    UserRating = 4
                },
                new Rating
                {
                    MovieId = schindlerId,
                    UserId = johnId,
                    UserRating = 2
                },
                new Rating
                {
                    MovieId = lotrId,
                    UserId = johnId,
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
