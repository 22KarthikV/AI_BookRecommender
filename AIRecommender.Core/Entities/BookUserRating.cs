﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Core.Entities
{
    public class BookUserRating
    {
        public int Rating { get; set; }
        public string ISBN { get; set; }
        public string UserID { get; set; }
    }
}
