using System.Text;
using System.Text.RegularExpressions;

namespace NewsPortalCMS.Shared.Utilities
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            title = title.ToLower().Trim();

            var sb = new StringBuilder();

            foreach (char c in title)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
                else if (char.IsWhiteSpace(c))
                {
                    sb.Append("-");
                }
            }

            var slug = Regex.Replace(sb.ToString(), "-+", "-");

            return slug.Trim('-');
        }
    }
}