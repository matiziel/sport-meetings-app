using System;
using Newtonsoft.Json;

namespace SportMeetingsApi.Utils;

public class Paging {
    public class Page {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
    
    private static readonly int maxPageSize = 40;
    private readonly Page _page;

    private Paging(Page page) {
        ValidatePage(page);
        _page = page;
    }

    public static Paging Create(Page page) => new Paging(page);

    private static void ValidatePage(Page page) {
        if (page.PageSize <= 0 || page.PageSize > maxPageSize || page.PageNumber < 0) {
            throw new ArgumentException($"Wrong pageSize|pageNumber ({JsonConvert.SerializeObject(page)})");
        }
    }

    public int Skip => _page.PageNumber * _page.PageSize;
    public int Take => _page.PageSize;
}
