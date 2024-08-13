using AIRecommender.Core.Entities;
using AIRecommender.Core.Implementations;
using AIRecommender.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace AIRecommender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Specify the paths to your CSV files
            string booksPath = @"C:\Users\Karthik.V\OneDrive - Consilio\Desktop\AI Recommender\books.csv";
            string ratingsPath = @"C:\Users\Karthik.V\OneDrive - Consilio\Desktop\AI Recommender\ratings.csv";
            string usersPath = @"C:\Users\Karthik.V\OneDrive - Consilio\Desktop\AI Recommender\users.csv";

            // Create instances of required classes
            IDataLoader dataLoader = new CSVDataLoader(booksPath, ratingsPath, usersPath);
            IRatingsAggregator ratingsAggregator = new RatingsAggregator();
            IRecommender recommender = new PearsonRecommender();

            // Create the AI Recommendation Engine
            var recommendationEngine = new AIRecommendationEngine(dataLoader, ratingsAggregator, recommender);

            // Example usage
            Preference userPreference = new Preference
            {
                ISBN = "6624330615",
                State = "State 45",
                Age = 23
            };

            int limit = 5; // Number of recommendations to retrieve

            try
            {
                Console.WriteLine("Searching through the files to retrive recommendations for you...Please Wait!");
                IList<Book> recommendations = recommendationEngine.Recommend(userPreference, limit);

                Console.WriteLine($"Top {limit} book recommendations for the user:");
                foreach (var book in recommendations)
                {
                    Console.WriteLine($"- {book.BookTitle} by {book.BookAuthor} (ISBN: {book.ISBN})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
