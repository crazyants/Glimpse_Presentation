﻿using NExtensions;

namespace Kiehl.App.Business
{
    public class SearchPager
    {
        public SearchPager()
        {
            Page = 1;
            PageSize = 10;
            IsActive = true;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string Search { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return "Search:{0} IsActive:{1} Page:{2} PageSize: {3}"
                .FormatWith(Search, IsActive, Page, PageSize);
        }
    }
}