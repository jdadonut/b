using System.Text;

namespace b.Extensions
{
    public static class TimesString
    {
        public static string Times(this string s, int times)
        {
            StringBuilder sb = new();
            for (int i = 0; i < times; i++)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }
    }
}