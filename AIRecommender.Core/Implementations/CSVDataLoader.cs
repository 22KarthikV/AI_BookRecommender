using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.Core.Entities;
using AIRecommender.Core.Interfaces;


namespace AIRecommender.Core.Implementations
{
    public class CSVDataLoader : IDataLoader
    {
        private readonly string _booksFilePath;
        private readonly string _ratingsFilePath;
        private readonly string _usersFilePath;

        public CSVDataLoader(string booksFilePath, string ratingsFilePath, string usersFilePath)
        {
            if (!File.Exists(booksFilePath) || !File.Exists(ratingsFilePath) || !File.Exists(usersFilePath))
            {
                throw new FileNotFoundException("One or more CSV files not found.");
            }
            _booksFilePath = booksFilePath;
            _ratingsFilePath = ratingsFilePath;
            _usersFilePath = usersFilePath;
        }

        public List<BookDetails> Load()
        {
            var books = LoadBooks();
            var ratings = LoadRatings();
            var users = LoadUsers();

            var bookDetails = new List<BookDetails>();

            foreach (var book in books)
            {
                var bookRatings = ratings.Where(r => r.ISBN == book.ISBN).ToList();
                var bookUsers = bookRatings.Select(r => users.FirstOrDefault(u => u.UserID == r.UserID)).Where(u => u != null).ToList();

                bookDetails.Add(new BookDetails
                {
                    Books = books,
                    UserRatings = bookRatings,
                    Users = bookUsers
                });
            }

            return bookDetails;
        }

        private List<Book> LoadBooks()
        {
            var books = new List<Book>();
            using (var reader = new StreamReader(_booksFilePath))
            {
                reader.ReadLine(); // Skip header
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    books.Add(new Book
                    {
                        ISBN = values[0],
                        BookTitle = values[1],
                        BookAuthor = values[2],
                        YearOfPublication = int.Parse(values[3]),
                        Publisher = values[4],
                        ImageUrlSmall = values[5],
                        ImageUrlMedium = values[6],
                        ImageUrlLarge = values[7]
                    });
                }
            }
            return books;
        }

        private List<BookUserRating> LoadRatings()
        {
            var ratings = new List<BookUserRating>();
            using (var reader = new StreamReader(_ratingsFilePath))
            {
                reader.ReadLine(); // Skip header
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    ratings.Add(new BookUserRating
                    {
                        UserID = values[0],
                        ISBN = values[1],
                        Rating = int.Parse(values[2])
                    });
                }
            }
            return ratings;
        }

        private List<User> LoadUsers()
        {
            var users = new List<User>();
            using (var reader = new StreamReader(_usersFilePath))
            {
                reader.ReadLine(); // Skip header
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    users.Add(new User
                    {
                        UserID = values[0],
                        Age = int.Parse(values[1]),
                        City = values[2],
                        State = values[3],
                        Country = values[4]
                    });
                }
            }
            return users;
        }
    }
}
