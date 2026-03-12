using System.Text;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Extensions
{
    public static class StringExt
    {
        private static StringBuilder s_builder = new StringBuilder();
        
        public static string FirstToLower(this string target)
        {
            string temp = $"{target[0]}";
            temp = temp.ToLower();
            s_builder.Append(temp);

            for (int i = 0; i < target.Length; i++)
            {
                if (i == 0)
                    continue;

                s_builder.Append(target[i]);
            }
            
            string result = s_builder.ToString();
            s_builder.Clear();
            
            return result;
        }
    }
}