using PagedList;

namespace Kiehl.App.Business
{
    public interface IPagedSearchResult<out T>
    {
        SearchPager Pager { get; }
        IPagedList<T> Items { get; }

    }
}
