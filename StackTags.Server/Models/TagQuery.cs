namespace StackTags.Server.Models
{
    public class TagQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string SortBy { get; set; } = "name";
        public string Order { get; set; } = "asc";
    }
}
