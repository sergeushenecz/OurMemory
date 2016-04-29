using System.Web;

namespace OurMemory.Common.Extention
{
    public static class StringExtention
    {
        static StringExtention()
        {
        }

        public static string ToAbsolutPath(this string relative)
        {
            return relative.Insert(0,
                HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority);
        }

        public static string ToRelativePath(this string absolut)
        {
            return absolut.Replace(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority, "");
        }
    }
}