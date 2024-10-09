using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagementLibrary.Models
{
    public class Movie
    {
        // Propeties
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
        public List<double> Rating { get; set; } // List is used for Maintaining Different Ratings
        public int Year { get; set; }

        public Movie() { } // used for Deserialization


        //Parameterized  Constructor
        public Movie(int movieId, string movieName, string genre, double rating, int year)
        {
            MovieId = movieId;
            MovieName = movieName;
            Genre = genre;
            Rating = new List<double> { rating };
            Year = year;
        }


        // Automatically Generates Movie ID if We want to apply then we can easily do this by GenerateMovieId();
        /* public string GenerateMovieId()
         {
             string timeStamp = DateTime.Now.ToString("ssmmhh");
             return $"MOV{timeStamp}";
         }*/


        //Override the ToSting Method for Display Movies
        public override string ToString()
        {
            return $" Movie ID : {MovieId}\n"
                + $" Movie Name : {MovieName}\n"
                + $" Genre : {Genre}\n"
                + $" Movie Rating : {Rating.Average():F2}\n"
                + $" Release Year : {Year}\n\n";
        }
    }
}
