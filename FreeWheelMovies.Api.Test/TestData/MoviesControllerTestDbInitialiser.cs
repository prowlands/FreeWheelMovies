using FreeWheelMovies.Database.Data;
using FreeWheelMovies.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeWheelMovies.Api.Test.TestData
{
    public static class MoviesControllerTestDbInitialiser
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
                    Title = "Pulp Fiction",
                    ReleaseYear = new DateTime(1994,1,1),
                    RunningTime = 154
                }
            };

            foreach (Movie m in moviesToAdd)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var shawshankId = context.Movies.Single(x => x.Title == "The Shawshank Redemption").Id;
            var godfatherId = context.Movies.Single(x => x.Title == "The Godfather").Id;
            var godfather2Id = context.Movies.Single(x => x.Title == "The Godfather - Part II").Id;
            var pulpFictionId = context.Movies.Single(x => x.Title == "Pulp Fiction").Id;

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
                new MovieGenre{ MovieId = godfather2Id, GenreId = historyId},
                new MovieGenre{ MovieId = godfather2Id, GenreId = dramaId },
            };

            foreach (MovieGenre mg in movieGenresToAdd)
            {
                context.MovieGenres.Add(mg);
            }
            context.SaveChanges();
            
        }
    }
}
