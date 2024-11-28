using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class CommonGridParams
    {
        [Range(1, int.MaxValue, ErrorMessage = $"Field {nameof(page)} Must be grater than zero.")]
        public int page { get; set; } = 1;

        [Range(0, int.MaxValue, ErrorMessage = $"Field {nameof(pageSize)} Must be a positive Number.")]
        public int pageSize { get; set; } = 10;
        public string? SearchKeyword { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public List<CommonFilterParams>? Filters { get; set; }

    }

    public class CommonFilterParams
    {
        public string? FieldName { get; set; } 
        public object? Value { get; set; }
        public string? Condition { get; set; } = "and";
        public string? Operator { get; set; } = "equals";
    }
}
