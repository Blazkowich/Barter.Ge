using Barter.Ge.BLL.Models.Enum;

namespace Barter.Ge.BLL.Models.Search.Context;

public class SearchContextBase
{
    public int PageNumber { get; set; } = 1;

    public int PerPage { get; set; } = 100;

    public SortingOptions SortDirection { get; set; }

    public string SortField { get; set; } = "Id";
}
