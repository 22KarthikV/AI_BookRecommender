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
    public class AIRecommendationEngine
    {
        private readonly IDataLoader _dataLoader;
        private readonly IRatingsAggregator _ratingsAggregator;
        private readonly IRecommender _recommender;

        public AIRecommendationEngine(IDataLoader dataLoader, IRatingsAggregator ratingsAggregator, IRecommender recommender)
        {
            _dataLoader = dataLoader;
            _ratingsAggregator = ratingsAggregator;
            _recommender = recommender;
        }

        public IList<Book> Recommend(Preference preference, int limit)
        {
            var bookDetails = _dataLoader.Load();
            var aggregatedRatings = _ratingsAggregator.Aggregate(bookDetails, preference);

            var correlations = new Dictionary<string, double>();
            var preferenceBook = bookDetails.SelectMany(bd => bd.Books).FirstOrDefault(b => b.ISBN == preference.ISBN);

            if (preferenceBook == null)
            {
                return new List<Book>();
            }

            foreach (var bookRatings in aggregatedRatings)
            {
                if (bookRatings.Key == preference.ISBN) continue;

                var baseRatings = aggregatedRatings[preference.ISBN][preference.State];
                var otherRatings = bookRatings.Value[preference.State];

                double correlation = _recommender.GetCorrelation(baseRatings.ToArray(), otherRatings.ToArray());
                correlations[bookRatings.Key] = correlation;
            }

            var recommendedBooks = correlations.OrderByDescending(c => c.Value)
                                               .Take(limit)
                                               .Select(c => bookDetails.SelectMany(bd => bd.Books)
                                                                       .FirstOrDefault(b => b.ISBN == c.Key))
                                               .Where(b => b != null)
                                               .ToList();

            return recommendedBooks;
        }
    }
}

