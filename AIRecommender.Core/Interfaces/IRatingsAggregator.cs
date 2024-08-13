using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIRecommender.Core.Entities;
using System.Collections.Generic;

namespace AIRecommender.Core.Interfaces
{
    public interface IRatingsAggregator
    {
        Dictionary<string, Dictionary<string, List<int>>> Aggregate(List<BookDetails> bookDetails, Preference preference);
    }
}
