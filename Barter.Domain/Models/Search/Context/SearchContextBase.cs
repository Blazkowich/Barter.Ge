using Barter.Domain.Models.Enum;

namespace Barter.Domain.Models.Search.Context;

public class SearchContextBase
{
    public int PageNumber { get; set; } = 1;

    public int PerPage { get; set; } = 100;

    public SortingOptions SortDirection { get; set; }

    public string SortField { get; set; } = "Id";
}
