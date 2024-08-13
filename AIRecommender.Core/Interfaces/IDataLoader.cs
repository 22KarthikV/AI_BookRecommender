using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIRecommender.Core.Entities;
using System.Collections.Generic;

namespace AIRecommender.Core.Interfaces
{
    public interface IDataLoader
    {
        List<BookDetails> Load();
    }
}
