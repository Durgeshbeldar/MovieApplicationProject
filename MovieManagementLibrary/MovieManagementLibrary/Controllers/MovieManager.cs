using MovieManagementLibrary.Exceptions;
using MovieManagementLibrary.Models;
using MovieManagementLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagementLibrary.Controllers
{
    public class MovieManager
    {
        List<Movie> movies;
        const int CAPACITY = 50; // Magic Number concept :) 5 Movies in MovieHub is so Low Number So I take 50 Movies Capacity

        // Construtor For Movie Manager
        public MovieManager()
        {
            movies = MovieSerializer.LoadMovies();
        }

        // To Get Movies List
        public List<Movie> GetMovies()
        {
            return movies;
        }


        // Save Data 
        public void SaveData()
        {
            MovieSerializer.SaveMovies(movies);
        }


        // Checking The Storage Capacity... For Now We Just Storing 50 Movies 
        public bool IsFull()
        {
            if (movies.Count >= CAPACITY) // Hanling the Logic of having 50 Movies only in the Movie Hub
                return true;
            return false;
        }


        // To Add Movies 
        public void AddMovie(int movieId, string movieName, string genre, double rating, int year)
        {
            movies.Add(new Movie(movieId, movieName, genre, rating, year));
            MovieSerializer.SaveMovies(movies);
        }


        // Find Movie By ID
        public string FindMovieById(string userInputId)
        {
            try
            {
                int movieId = int.Parse(userInputId);
                Movie movie = movies.FirstOrDefault(imovie => imovie.MovieId == movieId);
                if (movie == null)
                {
                    throw new MovieNotFoundException($"\nNo Movie Found With This ID: {movieId}");
                }
                return $"\nMovie Found: \n{movie.ToString()}";
            }
            catch (MovieNotFoundException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }


        // FInd Movie By name 
        public string FindMovieByName(string movieName)
        {
            try
            {
                Movie movie = movies.FirstOrDefault(imovie => imovie.MovieName.ToLower() == movieName);
                if (movie == null)
                {
                    throw new MovieNotFoundException($"\nNo Movie Found With This Name: {movieName}");
                }
                return $"\nMovie Found: \n{movie.ToString()}";
            }
            catch (MovieNotFoundException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }


        //Remove Movie By Id
        public string RemoveMovie(string userInput)
        {
            try
            {
                int movieId = int.Parse(userInput);
                Movie movie = movies.FirstOrDefault(imovie => imovie.MovieId == movieId);
                if (movie == null)
                {
                    throw new MovieNotFoundException($"\nNo Movie Found With This ID: {movieId}");
                }
                movies.Remove(movie);
                MovieSerializer.SaveMovies(movies);
                return $"\nThe {movie.MovieName} Movie is Removed Successfully!";
            }
            catch (MovieNotFoundException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }


        // Delete Movies All Movies at Once
        public void DeleteMovies()
        {
            movies.Clear();
            MovieSerializer.SaveMovies(movies);
        }


        //******* Edit Movies Section ********//


        // To Rate the Movie 
        public double RateMovie(int movieId, double userRating)
        {
            Movie toRate = movies.Find(movie => movie.MovieId == movieId);
            toRate.Rating.Add(userRating);
            MovieSerializer.SaveMovies(movies);
            return toRate.Rating.Average();
        }


        // Change Movie Name 
        public string ChangeMovieName(int movieId, string movieName)
        {
            Movie movie = movies.FirstOrDefault(imovie => imovie.MovieId == movieId);
            movie.MovieName = movieName;
            MovieSerializer.SaveMovies(movies);
            return "\nMovie Name is Updated Successfully... Thank You!";
        }


        // Change Movie Genre 
        public string ChangeMovieGenre(int movieId, string movieGenre)
        {
            Movie movie = movies.FirstOrDefault(imovie => imovie.MovieId == movieId);
            movie.Genre = movieGenre;
            MovieSerializer.SaveMovies(movies);
            return "\nMovie Genre is Updated Successfully... Thank You!";
        }


        // Change Movie Released Year 
        public string ChangeYear(int movieId, string userInput)
        {
            try
            {
                int newEditedYear = int.Parse(userInput);
                Movie movie = movies.FirstOrDefault(imovie => imovie.MovieId == movieId);
                movie.Year = newEditedYear;
                MovieSerializer.SaveMovies(movies);
                return $"\nThe Movie {movie.MovieName} Released Year is Changed to {newEditedYear} ...!";
            }
            catch (Exception ex)
            {
                return "\n" + ex.Message;
            }
        }
    }
}
