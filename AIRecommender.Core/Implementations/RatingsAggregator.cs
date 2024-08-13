using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIRecommender.Core.Entities;
using AIRecommender.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AIRecommender.Core.Implementations
{
    public class RatingsAggregator : IRatingsAggregator
    {
        public Dictionary<string, Dictionary<string, List<int>>> Aggregate(List<BookDetails> bookDetails, Preference preference)
        {
            var result = new Dictionary<string, Dictionary<string, List<int>>>();

            foreach (var bookDetail in bookDetails)
            {
                foreach (var book in bookDetail.Books)
                {
                    if (!result.ContainsKey(book.ISBN))
                    {
                        result[book.ISBN] = new Dictionary<string, List<int>>();
                    }

                    var relevantRatings = bookDetail.UserRatings
                        .Where(r => r.ISBN == book.ISBN)
                        .Join(bookDetail.Users,
                            r => r.UserID,
                            u => u.UserID,
                            (r, u) => new { Rating = r.Rating, User = u })
                        .Where(x => x.User.State == preference.State &&
                                    GetAgeGroup(x.User.Age) == GetAgeGroup(preference.Age))
                        .Select(x => x.Rating)
                        .ToList();

                    result[book.ISBN][preference.State] = relevantRatings;
                }
            }

            return result;
        }

        private string GetAgeGroup(int age)
        {
            if (age >= 1 && age <= 16) return "Teen Age";
            if (age >= 17 && age <= 30) return "Young Age";
            if (age >= 31 && age <= 50) return "Mid Age";
            if (age >= 51 && age <= 60) return "Old Age";
            return "Senior Citizens";
        }
    }
}



/*using AIRecommender.Core.Entities;
using AIRecommender.Core.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIRecommender.Core.Implementations
{
    public class RatingsAggregator : IRatingsAggregator
    {
        public Dictionary<string, Dictionary<string, List<int>>> Aggregate(List<BookDetails> bookDetails, Preference preference)
        {
            var result = new ConcurrentDictionary<string, Dictionary<string, List<int>>>();

            Parallel.ForEach(bookDetails, bookDetail =>
            {
                foreach (var book in bookDetail.Books)
                {
                    result.GetOrAdd(book.ISBN, _ => new Dictionary<string, List<int>>());

                    var relevantRatings = bookDetail.UserRatings
                        .Where(r => r.ISBN == book.ISBN)
                        .Join(bookDetail.Users,
                            r => r.UserID,
                            u => u.UserID,
                            (r, u) => new { Rating = r.Rating, User = u })
                        .Where(x => x.User.State == preference.State &&
                                    GetAgeGroup(x.User.Age) == GetAgeGroup(preference.Age))
                        .Select(x => x.Rating)
                        .ToList();

                    if (relevantRatings.Any())
                    {
                        result[book.ISBN][preference.State] = relevantRatings;
                    }
                }
            });

            return result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private string GetAgeGroup(int age)
        {
            if (age >= 1 && age <= 16) return "Teen Age";
            if (age >= 17 && age <= 30) return "Young Age";
            if (age >= 31 && age <= 50) return "Mid Age";
            if (age >= 51 && age <= 60) return "Old Age";
            return "Senior Citizens";
        }
    }
}*/