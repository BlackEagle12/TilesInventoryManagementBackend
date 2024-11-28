using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public static class SQLFunctions // SqlServerDbFunctionsExtensions
    {
        public static bool Like(string str1, string str2)
        {
            return EF.Functions.Like(str1, str2);
        }

        public static bool Contains(string str1, string str2)
        {
            return EF.Functions.Contains(str1, str2);
        }
    }
}
