using MovieManagementLibrary.Controllers;
using MovieManagementLibrary.Exceptions;
using MovieManagementLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApplication.Presentation
{
    public class MovieHub
    {
        public static MovieManager movieManager;

        static MovieHub()
        {
            try
            {
                movieManager = new MovieManager();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing MovieManager: {ex.Message}");
                throw;
            }
        }

        //public MovieManager movieManager = new MovieManager();


        // MovieHub Main Menu
        public static void MoviesApp()
        {
            Console.WriteLine("\n***************** Welcome to MoviesHub Application *****************\n");
            while (true)
            {
                // Menu Implementation...
                Console.WriteLine(
                    $"\n*** Welcome to MovieHub Main Menu ***\n" +
                    $"Choose The Options From The Following List : \n\n"
                        + $" 1. Add New Movie\n"
                        + $" 2. Edit Movie\n"
                        + $" 3. Find Movie By Id\n"
                        + $" 4. Find Movie By Name\n"
                        + $" 5. Display All Movies\n"
                        + $" 6. Remove Movie By Id\n"
                        + $" 7. Clear All Movies\n"
                        + $" 8. Exit/Cancel\n"
                );

                int option = int.Parse(Console.ReadLine());
                ExecuteMenuOptions(option);
            }
        }


        // Menu Operator Function 
        public static void ExecuteMenuOptions(int option)
        {
            switch (option)
            {
                case 1:
                    AddMovie();
                    break;
                case 2:
                    EditMovies();
                    break;
                case 3:
                    FindMovieById();
                    break;
                case 4:
                    FindMovieByName();
                    break;
                case 5:
                    DisplayMovies();
                    break;
                case 6:
                    RemoveMovieById();
                    break;
                case 7:
                    ConfirmAndDeleteAllMovies();
                    break;
                case 8:
                    SaveMovies();
                    Console.WriteLine("Exited Successfully....!");
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid Input, Please Try Again\n");
                    break;
            }
        }


        // To add the Movie
        public static void AddMovie()
        {
            try
            {
                if (movieManager.IsFull())
                    throw new MovieHubCapacityFullException("Cannot Add More Movies. The MovieHub has Reached its Maximum Capacity of 50 Movies.");
                Console.WriteLine("Enter The Movie Id :");
                int movieId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter The Movie Name :");
                string movieName = Console.ReadLine();

                Console.WriteLine("Enter Genre : ");
                string genre = Console.ReadLine();

                Console.WriteLine("Enter the Rating : ");
                double rating = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter Movie Released Year :");
                int year = int.Parse(Console.ReadLine());

                movieManager.AddMovie(movieId, movieName, genre, rating, year);
                Console.WriteLine("\nMovie Added Successfully to MovieHub Store...!");
            }
            catch (MovieHubCapacityFullException CapacityException)
            {
                Console.WriteLine(CapacityException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
                AddMovie();
            }
        }


        // Find Movie By Id 
        public static void FindMovieById()
        {
            Console.WriteLine("Enter The Movie Id : ");
            string movieId = Console.ReadLine();
            Console.WriteLine(movieManager.FindMovieById(movieId));
        }

        //Find Movie By Name 
        public static void FindMovieByName()
        {
            Console.WriteLine("Enter The Movie Name :");
            string movieName = Console.ReadLine().ToLower();
            Console.WriteLine(movieManager.FindMovieByName(movieName));
        }


        // Used For Saving the Data
        public static void SaveMovies()
        {
            movieManager.SaveData();
            Console.WriteLine("All Data Saved Successfully...!");
        }


        // To Display the Movies ...
        public static void DisplayMovies()
        {
            List<Movie> movies = movieManager.GetMovies();
            try
            {
                if (movies.Count == 0)
                    throw new EmptyMovieHubException("The MovieHub is Empty... No Movies To Display!");
                int index = 1;
                Console.WriteLine("\n****** All Movie List ******\n");
                foreach (Movie movie in movies)
                    Console.WriteLine($" {index++}.\n{movie.ToString()}");
            }
            catch (EmptyMovieHubException NoMovieException)
            {
                Console.WriteLine(NoMovieException.Message);
            }
        }


        // For Removing the Movie By Id
        public static void RemoveMovieById()
        {
            Console.WriteLine("Enter The Movie ID :");
            string movieId = Console.ReadLine();
            Console.WriteLine(movieManager.RemoveMovie(movieId));
        }


        // To Clear or Delete All Movies 
        public static void ConfirmAndDeleteAllMovies()
        {
            try
            {
                if (movieManager.GetMovies().Count == 0)
                    throw new EmptyMovieHubException("The Movie Collection/Hub is Empty, There Are No Movies to Delete");
                Console.WriteLine("Are You Sure, Do You Want To Delete All Movies? Please Type Yes or No (y/n)");
                string userInput = Console.ReadLine().ToLower();
                if (userInput != "y" && userInput != "n")
                {
                    throw new InvalidInputException("Invalid Input, Please Give Correct Input : 'y' For Yes 'n' For No");
                }
                TryToDeleteMovies(userInput);

            }
            catch (EmptyMovieHubException NoMovieException)
            {
                Console.WriteLine(NoMovieException.Message);
            }
            catch (InvalidInputException InvalidInput)
            {
                Console.WriteLine(InvalidInput.Message);
                ConfirmAndDeleteAllMovies();
            }
        }

        // Trying To Delete if User Said Yes...
        public static void TryToDeleteMovies(string userInput)
        {
            if (userInput == "y")
            {
                movieManager.DeleteMovies();
                Console.WriteLine("All Movies Deleted Successfully...!");
                return;
            }
            Console.WriteLine("Redirecting You To Movie Hub...Thank You!");
        }



        // Operate Individual Movie Or Edit the Movie
        public static void EditMovies()
        {
            List<Movie> movies = movieManager.GetMovies();
            Movie selectedMovie = SelectMovie(movies);
            bool operateMovies = true;
            while (operateMovies)
            {
                Console.WriteLine(
                    $"\nChoose The Options From The Following List : \n\n"
                        + $" 1. Show Movie Details \n"
                        + $" 2. Rate The Movie\n"
                        + $" 3. Change Movie Name\n"
                        + $" 4. Change Genre\n"
                        + $" 5. Change Movie Release Year\n"
                        + $" 6. Exit/Cancel\n"
                );
                int option = int.Parse(Console.ReadLine());
                ExecuteMovieOptions(selectedMovie, option, ref operateMovies);
            }
        }


        // To select the Movie
        public static Movie SelectMovie(List<Movie> movies)
        {
            if (movies.Count == 0)
                return null;
            int index;
            for (index = 0; index < movies.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {movies[index].MovieName}");
            }
            int selectMovie = GetUserSelection(movies.Count);
            return movies[selectMovie - 1];
        }


        // Method to get user selection
        public static int GetUserSelection(int max)
        {
            Console.WriteLine("\nSelect The Movie From The Above List :");
            while (true)
            {
                try
                {
                    int select = int.Parse(Console.ReadLine());
                    if (select > 0 && select <= max)
                        return select;
                    else
                        throw new InvalidInputException($"Invalid Input, Please Enter The Number Between 1 to {max}");
                }
                catch (InvalidInputException invalidInput)
                {
                    Console.WriteLine(invalidInput.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        // You Can Perform Edit, Read & Update Operations From this user-interface 
        public static void ExecuteMovieOptions(Movie selectedMovie, int option, ref bool operate)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine(selectedMovie.ToString());
                    break;
                case 2:
                    RateTheMovie(selectedMovie.MovieId);
                    break;
                case 3:
                    ChangeMovieName(selectedMovie.MovieId);
                    break;
                case 4:
                    ChangeMovieGenre(selectedMovie.MovieId);
                    break;
                case 5:
                    ChangeMovieReleasedYear(selectedMovie.MovieId);
                    break;
                case 6:
                    Console.WriteLine("Exited Successfully...!");
                    operate = false;
                    return;
                default:
                    Console.WriteLine("Invalid Input, Please Select the Correct Option\n");
                    break;
            }
        }


        // To Rate the movie the following function will be use...
        public static void RateTheMovie(int movieId)
        {
            double movieRating = movieManager.RateMovie(movieId, GetValidRating());
            Console.WriteLine($"Rating Submitted Successfully!, New Rating is : {movieRating:F2}\n");
        }


        // to take valid rating from user ...
        public static double GetValidRating()
        {
            Console.WriteLine("Enter Your Rating Between (1 to 5) : ");
            try
            {
                double userRating = double.Parse(Console.ReadLine());
                if (userRating >= 1 && userRating <= 5)
                    return userRating;
                throw new InvalidInputException("Please Enter the Valid Rating**");
            }
            catch (InvalidInputException invalidInput)
            {
                Console.WriteLine(invalidInput.Message);
                return GetValidRating();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return GetValidRating();
            }
        }

        // To change The Movie Name
        public static void ChangeMovieName(int movieId)
        {
            try
            {
                Console.WriteLine("Enter New Movie Name : ");
                string userInput = Console.ReadLine();
                Console.WriteLine(movieManager.ChangeMovieName(movieId, userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // To Change Movie Genre
        public static void ChangeMovieGenre(int movieId)
        {
            try
            {
                Console.WriteLine("Enter New Genre For Selected Movie : ");
                string userInput = Console.ReadLine();
                if (!IsValidGenre(userInput))
                {
                    throw new InvalidInputException("Invalid Genre Name, Please Enter Only Letters and Spaces");
                }
                Console.WriteLine(movieManager.ChangeMovieGenre(movieId, userInput));
            }
            catch (InvalidInputException InvalidInput)
            {
                Console.WriteLine(InvalidInput.Message);
            }
        }
        public static bool IsValidGenre(string userInput)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(userInput, @"^[a-zA-Z\s]+$");
        }


        // Changer Movie Released Year 
        public static void ChangeMovieReleasedYear(int movieId)
        {
            Console.WriteLine("Enter The Movie Released Year : ");
            string userInput = Console.ReadLine();
            Console.WriteLine(movieManager.ChangeYear(movieId, userInput));
        }
    }
}
