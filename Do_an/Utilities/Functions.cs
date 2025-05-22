using System.Security.Cryptography;
using System.Text;

namespace Do_an.Utilities
{
    public class Functions
    {
        public static int _UserID = 0;
        public static string _Username = String.Empty;
        public static string _Email = String.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;

        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }

        public static string MD5Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text), "Dữ liệu đầu vào không được null hoặc rỗng.");
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static string MD5Password(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text), "Mật khẩu không được null hoặc rỗng.");
            }

            string str = MD5Hash(text);
            for (int i = 0; i <= 5; i++)
                str = MD5Hash(str + "_" + str);
            return str;
        }

        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Functions._Username) || string.IsNullOrEmpty(Functions._Email) || (Functions._UserID <= 0))
                return false;
            return true;
        }
    }
}