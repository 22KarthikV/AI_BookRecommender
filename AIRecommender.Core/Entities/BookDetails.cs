using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace AIRecommender.Core.Entities
{
    public class BookDetails
    {
        public List<Book> Books { get; set; }
        public List<BookUserRating> UserRatings { get; set; }
        public List<User> Users { get; set; }

        public BookDetails()
        {
            Books = new List<Book>();
            UserRatings = new List<BookUserRating>();
            Users = new List<User>();
        }
    }
}