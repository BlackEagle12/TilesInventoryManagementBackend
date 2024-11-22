
namespace Dto
{
    public class CommonDto : IPagination
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
