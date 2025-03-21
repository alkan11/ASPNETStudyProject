﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeature
{
    public abstract  class RequestParameters
    {
        const int maxPageSize = 50;

        public int PageNumber { get; set; }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }


    }

    public class BookParameters : RequestParameters
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public bool ValidPriceRange => MaxPrice > MinPrice;
    }
}
